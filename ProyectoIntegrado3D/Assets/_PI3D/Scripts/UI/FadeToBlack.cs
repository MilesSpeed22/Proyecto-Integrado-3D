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

    public IEnumerator FadingIn()
    {
        float inTime = 0;

        Color inColor = fadeImage.color;

        while (inTime < fadeDuration)
        {
            inTime += Time.deltaTime;

            inColor.a = 1 - (inTime / fadeDuration);

            fadeImage.color = inColor;

            yield return null;
        }

        inColor.a = 0;

        fadeImage.color = inColor;
    }
}
