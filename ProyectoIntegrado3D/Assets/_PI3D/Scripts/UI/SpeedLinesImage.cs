using UnityEngine;
using UnityEngine.UI;

public class SpeedLinesUI : MonoBehaviour
{
    [SerializeField] Image speedLines;
    [SerializeField] float minSpeed = 10f;
    [SerializeField] float maxSpeed = 50f;
    [SerializeField] float maxAlpha = 0.8f;
    void Update()
    {
        float speed = TilesMovement.speed;

        float normalizedSpeed = Mathf.InverseLerp(minSpeed, maxSpeed, speed);

        Color color = speedLines.color;
        float targetAlpha = normalizedSpeed * maxAlpha;
        color.a = Mathf.Lerp(color.a, targetAlpha, Time.deltaTime * 5f);
        speedLines.color = color;
    }
}
