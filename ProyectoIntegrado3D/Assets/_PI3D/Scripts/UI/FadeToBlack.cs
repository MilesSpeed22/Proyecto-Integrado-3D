using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeDuration = 1f;
    
    public IEnumerator FadingToBlack()
    {
        float time = 0;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = time / fadeDuration;
            fadeImage.color = color;

            yield return null;
        }

        color.a = 1;

        fadeImage.color = color;
    }
}
