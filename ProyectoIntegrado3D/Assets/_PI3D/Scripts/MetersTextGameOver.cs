using TMPro;
using UnityEngine;

public class MetersTextGameOver : MonoBehaviour
{
    [SerializeField] TextMeshPro metersTraveled;
    void Start()
    {
        int distance = PlayerPrefs.GetInt("Distance", 0);

        metersTraveled.text = distance + "m"; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
