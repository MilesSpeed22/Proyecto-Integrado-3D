using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [SerializeField] AudioClip[] musicList;
    [SerializeField] AudioClip[] sfxList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void PlayMusic(int musicIndex)
    {
        musicSource.clip = musicList[musicIndex];
        musicSource.Play();
    }

    public void PlaySFX(int sfxIndex)
    {
        sfxSource.PlayOneShot(sfxList[sfxIndex]);
    }
}
