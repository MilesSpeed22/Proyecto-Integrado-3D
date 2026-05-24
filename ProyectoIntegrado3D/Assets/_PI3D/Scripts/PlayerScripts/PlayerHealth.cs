using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth = 3;
    [SerializeField] GameObject camObject;
    [SerializeField] CameraShake camShake;
    [SerializeField] Animator anim;
    bool isDead = false;
    [SerializeField] TileGenerator tileGenerator;
    [SerializeField] FadeToBlack fadeToBlack;
    [SerializeField] SkinnedMeshRenderer playerMesh;
    void Start()
    {
        StartCoroutine(fadeToBlack.FadingIn());
        health = maxHealth;
        camShake = camObject.GetComponent<CameraShake>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            health--;
            AudioManager.Instance.PlaySFX(0);
            AudioManager.Instance.PlaySFX(1);
            StartCoroutine(Blink());
            StartCoroutine(camShake.Shake(0.15f, 0.2f));
        }

        if (health <= 0)
        {
            StartCoroutine(DeathCoroutine());
        }
    }

    
    IEnumerator DeathCoroutine()
    {
        isDead = true;

        PlayerPrefs.SetInt("Distance", tileGenerator.metersCount);

        GetComponent<PlayerController>().canMove = false;

        AudioManager.Instance.PlaySFX(3);

        TilesMovement.speed = 0f;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;

        anim.SetTrigger("Death");

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(fadeToBlack.FadingToBlack());
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(2);
    }

    IEnumerator Blink()//182 :)
    {
        for (int i = 0; i < 5; i++)
        {
            playerMesh.enabled = false;

            yield return new WaitForSeconds(0.1f);

            playerMesh.enabled = true;

            yield return new WaitForSeconds(0.1f);
        }
    }

}
