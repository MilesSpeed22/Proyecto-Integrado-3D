using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] FadeToBlack fadeToBlack;
    public void LoadScene(int sceneToLoad)
    {
        StartCoroutine(fadeToBlack.FadingIn());
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
