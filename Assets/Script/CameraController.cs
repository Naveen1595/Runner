using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform Player;
    private Camera mainCamera;
    float zPosition;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        zPosition = Player.transform.position.z - 600f; 
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, zPosition);
    }
}
