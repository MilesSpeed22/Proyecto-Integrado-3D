using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointsSystem : MonoBehaviour
{
    public int points;
    public int winPoints;
    public GameObject winObject;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        winPoints = 4;
        winObject = GameObject.Find("WinPick");
        winObject.SetActive(false);
    }

    private void Update()
    {
        if (points >= winPoints) { winObject.SetActive(true); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            points += 1;
            other.gameObject.SetActive(false);
        }
        if(other.gameObject.CompareTag("WinPick"))
        {
            SceneManager.LoadScene(1);
        }
    }
}
