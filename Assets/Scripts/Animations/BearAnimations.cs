using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Collections;
using Unity.VisualScripting;
public class BearAnimations : MonoBehaviour
{
    [SerializeField] private GameObject Bear;
    [SerializeField] private int walkPerSecond = 3;
    [SerializeField] private int runPerSecond = 10;
    [SerializeField] private string idleAnimation = "Idle";
    [SerializeField] private string walkingAnimation = "WalkForward";
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

    private AnimationFunctions animationFunctions;
    private void Start()
    {
        animationFunctions = new AnimationFunctions();

        Animator animator = Bear.GetComponent<Animator>();

        //StartCoroutine(animationFunctions.MobGoTo(walkingAnimation, idleAnimation, Bear, walkPerSecond, 10, "E"));
        //StartCoroutine(MobGoE(walkingAnimation, idleAnimation, Bear, walkPerSecond, 3f));

        //animator.SetBool("Attack1", true);
    }

}
