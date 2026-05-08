using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth = 3;
    [SerializeField] GameObject camObject;
    [SerializeField] CameraShake camShake;
    void Start()
    {
        health = maxHealth;
        camShake = camObject.GetComponent<CameraShake>();
    }
    void Update()
    {
        Death();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            health--;

            StartCoroutine(camShake.Shake(0.15f, 0.2f));
        }
    }

    void Death()
    {
        if (health <= 0) gameObject.SetActive(false);
    }
}
