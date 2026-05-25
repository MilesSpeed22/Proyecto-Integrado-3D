using UnityEngine;

public class SFXTrigger : MonoBehaviour
{
    [SerializeField] int SFXToPlay;
    void Start()
    {
        AudioManager.Instance.PlaySFX(SFXToPlay);
    }
}
