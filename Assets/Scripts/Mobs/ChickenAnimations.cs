using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimations : MonoBehaviour
{
    [SerializeField] private GameObject Chicken;
    [SerializeField] private int walkPerSecond = 1;
    [SerializeField] private int runPerSecond = 5;
    [SerializeField] private string idle = "Idle";
    [SerializeField] private string walking = "Walk";
    [SerializeField] private string attack1 = "Attack1";
    [SerializeField] private string attack2 = "Attack2";
    [SerializeField] private string buff = "Buff";
    [SerializeField] private string eat = "Eat";
    [SerializeField] private string sleep = "Sleep";
    [SerializeField] private string death = "Death";
    [SerializeField] private string stun = "Stun";
    public bool isMoving;
    private bool vegan = true;
    AnimationFunctions animationFunctions;
    GlobalStatesAndFunctions globalStatesAndFunctions;
    private void Start()
    {
        animationFunctions = FindObjectOfType<AnimationFunctions>();
        globalStatesAndFunctions = new GlobalStatesAndFunctions();
        InvokeRepeating("CheckThisObjectPosition", 0f, 1f);
        PeaceState();
    }

    private void PeaceState()
    {
        new WaitForSeconds(0.1f);
        StartCoroutine(animationFunctions.AiPeaceState(Chicken, walkPerSecond, vegan, idle, walking, buff, eat, sleep, eat));
    }
    private void CheckThisObjectPosition()
    {
        globalStatesAndFunctions.CheckPosition(Chicken);
    }
    
}
