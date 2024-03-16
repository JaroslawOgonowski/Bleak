using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Collections;
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

    private AnimationFunctions animationFunctions;
    private void Start()
    {
        animationFunctions = new AnimationFunctions();

        Animator animator = Bear.GetComponent<Animator>();

        //StartCoroutine(animationFunctions.MobGoTo(walkingAnimation, idleAnimation, Bear, walkPerSecond, 10, "E"));
        //StartCoroutine(MobGoE(walkingAnimation, idleAnimation, Bear, walkPerSecond, 3f));

        //animator.SetBool("Attack1", true);
    }

    private bool isMoving = false;

    private void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isMoving = true;
                StartCoroutine(MoveCharacter("N"));
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                isMoving = true;
                StartCoroutine(MoveCharacter("W"));
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                isMoving = true;
                StartCoroutine(MoveCharacter("S"));
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                isMoving = true;
                StartCoroutine(MoveCharacter("E"));
            }
        }

        // Obs³uga zatrzymywania ruchu po puszczeniu klawisza
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            isMoving = false;
        }
    }

    private IEnumerator MoveCharacter(string direction)
    {
        // Tutaj umieœæ kod odpowiedzialny za ruch postaci

        // Zaczekaj na zakoñczenie animacji ruchu
        while (isMoving)
        {
            StartCoroutine(animationFunctions.MobGoTo(walkingAnimation, Bear, walkPerSecond, 0.1f, direction));
            yield return new WaitForSeconds(1f); // Czas oczekiwania na zakoñczenie animacji
        }
    }



}
