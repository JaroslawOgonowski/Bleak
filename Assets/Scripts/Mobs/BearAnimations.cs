using UnityEngine;
using System.Collections;

public class BearAnimations : MonoBehaviour
{
    [SerializeField] private GameObject Bear;
    [SerializeField] private int walkPerSecond = 3;
    [SerializeField] private int runPerSecond = 10;
    [SerializeField] private string idle = "Idle";
    [SerializeField] private string walking = "WalkForward";
    [SerializeField] private string runningAnimation = "Run Forward";
    [SerializeField] private string jumpAnimation = "Jump";
    [SerializeField] private string attack1 = "Attack1";
    [SerializeField] private string attack2 = "Attack2";
    [SerializeField] private string attack3 = "Attack3";
    [SerializeField] private string attack5 = "Attack5";
    [SerializeField] private string buff = "Buff";
    [SerializeField] private string eat = "Eat";
    [SerializeField] private string sit = "Sit";
    [SerializeField] private string sleep = "Sleep";
    [SerializeField] private string death = "Death";
    [SerializeField] private string getHit = "Get Hit Front";
    [SerializeField] private string stun = "Stunned Loop";
    public bool isMoving;
    private bool vegan = false;
    AnimationFunctions animationFunctions;
    GlobalStatesAndFunctions globalStatesAndFunctions;
    private void Start()
    {
        animationFunctions = FindObjectOfType<AnimationFunctions>();
        globalStatesAndFunctions = new GlobalStatesAndFunctions();
        InvokeRepeating("CheckThisObjectPosition", 0f, 1f);
        PeaceState();
    }

   private void CheckThisObjectPosition()
    {
        globalStatesAndFunctions.CheckPosition(Bear);
    }
    private void PeaceState()
    {
        new WaitForSeconds(0.1f);
        StartCoroutine(animationFunctions.AiPeaceState(Bear, walkPerSecond, vegan, idle, walking, buff, eat, sleep, sit));
    }
}
