
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObstacleAvoidance : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent nav;
    private Vector3 targetDir;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        targetDir = player.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        if (angle >= -70 && angle <= 70)
        {
            nav.SetDestination(player.position);
        }
    }
}
