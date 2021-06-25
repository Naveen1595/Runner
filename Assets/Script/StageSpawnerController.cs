using UnityEngine;

public class StageSpawnerController : MonoBehaviour
{
    
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Stage;
    bool isGenerate = false;
    private Vector3 nextStagePosition;
    private PlayerMovement playerMovement;
    
   

    private void Awake()
    {
        playerMovement = Player.GetComponent<PlayerMovement>();
        playerMovement.StageGenerator += GenerateStage;
    }

    private void Start()
    {
        nextStagePosition.z = 11200f;   
        
    }


    private void Update()
    {

        if (isGenerate)
        {
            isGenerate = false;
            Instantiate(Stage, nextStagePosition, Stage.transform.rotation);
            nextStagePosition.z += 5600f;
        }

        
    }

    void GenerateStage()
    {
        isGenerate = true;
    }

    

}
