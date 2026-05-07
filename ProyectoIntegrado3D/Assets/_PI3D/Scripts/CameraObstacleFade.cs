using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CameraObstacleFade : MonoBehaviour
{
    [SerializeField] Transform player;

    HashSet<FadeObstacle> currentObstacle = new HashSet<FadeObstacle>();
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        
        float distance = direction.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction.normalized, distance);

        HashSet<FadeObstacle> newObstacles = new HashSet<FadeObstacle>();

        foreach (FadeObstacle obstacle in currentObstacle)
        {
            if (!newObstacles.Contains(obstacle))
            {
                obstacle.SetFade(false);
            }
        }

        currentObstacle = newObstacles;
    }

    

   
}
