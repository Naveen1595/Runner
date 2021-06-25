using UnityEngine;

public class CollectableController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>() !=null)
        {
            gameObject.SetActive(false);
        }
    }
}
