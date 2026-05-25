using UnityEngine;

public class Text : MonoBehaviour
{
    [SerializeField] GameObject text;
    private void OnTriggerStay(Collider other)
    {
        text.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }

    private void Update()
    {
        text.transform.Rotate(180, 0, 0);
        text.transform.LookAt(Camera.main.transform);
    }
}
