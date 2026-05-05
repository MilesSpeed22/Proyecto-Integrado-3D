using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth = 3;
    void Start()
    {
        health = maxHealth;
    }
    void Update()
    {
        Death();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            health--;
        }
    }

    void Death()
    {
        if (health <= 0) gameObject.SetActive(false);
    }
}
