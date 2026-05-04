using UnityEngine;

public class TilesMovement : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
