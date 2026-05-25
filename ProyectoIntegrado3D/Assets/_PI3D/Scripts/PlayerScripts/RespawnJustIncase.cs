using Unity.VisualScripting;
using UnityEngine;

public class RespawnJustIncase : MonoBehaviour
{
    [SerializeField] Transform respawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = respawnPosition.transform.position;
        }
    }
}
