using UnityEngine;

public class FadeObstacle : MonoBehaviour
{
    Renderer[] renderers;

    [Header("Fade Settings")]
    [SerializeField] float fadeAlpha = 0.3f;
    [SerializeField] float fadeSpeed = 8f;

    bool isFading = false;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            rend.material = new Material(rend.material);
        }
    }

    private void Update()
    {
        float targetAlpha = isFading ? fadeAlpha : 1f;

        foreach (Renderer rend in renderers)
        {
            Color color = rend.material.color;

            color.a = Mathf.Lerp(color.a, targetAlpha, Time.deltaTime * fadeSpeed);
        }
    }

    public void SetFade(bool fade)
    {
        isFading = fade;
    }
}
