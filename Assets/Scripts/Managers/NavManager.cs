using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavManager : MonoBehaviour
{

    [Header("NavMesh Configurations")]
    public float AvoidancePredictionTime = 2;
    public int PathfindingIterationsPerFrame = 100;

    void Start()
    {
        NavMesh.avoidancePredictionTime = AvoidancePredictionTime;
        NavMesh.pathfindingIterationsPerFrame = PathfindingIterationsPerFrame;
    }


}
