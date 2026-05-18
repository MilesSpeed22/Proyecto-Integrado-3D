using UnityEngine;

public class TilesMovement : MonoBehaviour
{
    public static float speed = 10f;

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
