using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum playerPosition
{
    Left,
    Mid,
    Right
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject[] Lifeline;
    private int noOfHeart = 3;
    [SerializeField]private GameObject GameOverScreen;
    playerPosition currentPos = playerPosition.Mid;
    private float zPosition;
    private float xCharPosition;
    [SerializeField] private float speed = 5f;
    private bool GenerateStage = false;
    bool LeftControl;
    bool RightControl;
    bool JumpControl;
    bool SlideControl;
    bool isJump = false;
    bool isSlide = false;
    private Animator playerAnimator;
    [SerializeField]private float trackChangeSpeed = 10f;
    private float trackChange;
    [SerializeField]private float jumpForce = 10f;
    private float jump;
    private CharacterController charMotionControl;
    private float slidePlayerColliderHeight;
    private Vector3 slidePlayerColliderCenter;
    private float PlayerColliderHeight;
    private Vector3 PlayerColliderCenter;
    private Vector3 resetPosition = new Vector3(0f, 0f, -2410f);
    int Score = 0;
    bool isDeath =false;

    public event Action StageGenerator;
 
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Obstacle")
        {
            gameObject.transform.position = resetPosition;
            if (noOfHeart > 0)
            {
                --noOfHeart;
                Lifeline[noOfHeart].SetActive(false);
                speed = 0f;
                StartCoroutine("RePosition");
            }
            else
            {
                speed = 0f;
                isDeath = true;
                GameOverScreen.SetActive(true);
            }


        }


    }

    IEnumerator RePosition()
    {
        yield return new WaitForSeconds(2f);
        speed = 30f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StartStage")
        {

            StageGenerator?.Invoke();
        }

        if(other.gameObject.tag == "Collectables")
        {
            Score += 10;
            scoreText.text = "SCORE : " + Score;
        }
    }

    private void Start()
    {
        charMotionControl = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        PlayerColliderHeight = playerCollider.height;
        PlayerColliderCenter = playerCollider.center;
        slidePlayerColliderHeight = playerCollider.height/2;
        slidePlayerColliderCenter = new Vector3(playerCollider.center.x, playerCollider.center.y-30f, playerCollider.center.z);
    }

    private void Update()
    {
        //Player Movement
        LeftControl = Input.GetKeyDown(KeyCode.LeftArrow);
        RightControl = Input.GetKeyDown(KeyCode.RightArrow);
        JumpControl = Input.GetKeyDown(KeyCode.UpArrow);
        SlideControl = Input.GetKeyDown(KeyCode.DownArrow);

        if (LeftControl)
        {
            LeftMovement();
            playerAnimator.Play("Left_Anim");
        }
        else if (RightControl)
        {
            RightMovement();
            playerAnimator.Play("Right_Anim");
        }
        else if(JumpControl)
        {
            JumpMovement();
            playerAnimator.Play("Jump");
        }
        else if(SlideControl)
        {
            SlideMovement();
            playerAnimator.Play("Slide_Anim");
        }

        if(!isDeath)
        {
            zPosition = gameObject.transform.position.z + speed;
        }

        Vector3 charMove = new Vector3(trackChange - transform.position.x, jump, 0);
        trackChange = Mathf.Lerp(trackChange, xCharPosition, Time.deltaTime* trackChangeSpeed);
        
        charMotionControl.Move(charMove * Time.deltaTime);
    }


    private void FixedUpdate()
    {
        //Camera Movement
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,zPosition);
        
    }

    //LeftMovement
    void LeftMovement()
    {
        if(!isJump && !isSlide)
        {
            if (currentPos == playerPosition.Mid)
            {
                LeftPos();

                currentPos = playerPosition.Left;
                xCharPosition = -200;
            }
            else if (currentPos == playerPosition.Right)
            {
                LeftPos();
                currentPos = playerPosition.Mid;
                xCharPosition = 0;
            }
        }
        
    }

    //RightMovement
    void RightMovement()
    {
        if(!isJump && !isSlide)
        {
            if (currentPos == playerPosition.Mid)
            {
                RightPos();
                currentPos = playerPosition.Right;
                xCharPosition = 200;
            }
            else if (currentPos == playerPosition.Left)
            {
                RightPos();
                currentPos = playerPosition.Mid;
                xCharPosition = 0;
            }
        }
        
    }

    void JumpMovement()
    {
        
        if(charMotionControl.isGrounded)
        {
            jump = jumpForce;
            isJump = true;
            Debug.Log("IsGrounded");
        }
        else
        {
            //jump -= jumpForce * Time.deltaTime;
            isJump = false;
            Debug.Log("Not on ground");
        }
        
    }

    void SlideMovement()
    {
        if (!isJump && !isSlide)
        {
            isSlide = true;
            gameObject.GetComponent<CapsuleCollider>().height = 55f;
            gameObject.GetComponent<CapsuleCollider>().center = new Vector3(gameObject.GetComponent<CapsuleCollider>().center.x, gameObject.GetComponent<CapsuleCollider>().center.y - 20f, gameObject.GetComponent<CapsuleCollider>().center.z);
            playerCollider.height = slidePlayerColliderHeight;
            playerCollider.center = slidePlayerColliderCenter;
            
            StartCoroutine("slideTime");

            isSlide = false;
            
        }
        
    }
    IEnumerator slideTime()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<CapsuleCollider>().height = 110f;
        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(gameObject.GetComponent<CapsuleCollider>().center.x, gameObject.GetComponent<CapsuleCollider>().center.y + 20f, gameObject.GetComponent<CapsuleCollider>().center.z);
        playerCollider.height = PlayerColliderHeight;
        playerCollider.center = PlayerColliderCenter;
        
    }

    //Left Position
    void LeftPos()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x - 200f, gameObject.transform.position.y, gameObject.transform.position.z);
        
    }

    //Right Position
    void RightPos()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + 200f, gameObject.transform.position.y, gameObject.transform.position.z);
        
    }
    
    
    
}
