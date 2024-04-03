using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimations : MonoBehaviour
{
    [SerializeField] private GameObject Chicken;
    [SerializeField] private int walkPerSecond = 2;
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
        animationFunctions = new AnimationFunctions();
        globalStatesAndFunctions = new GlobalStatesAndFunctions();
        InvokeRepeating("CheckThisObjectPosition", 0f, 1f);
        StartCoroutine(AiPeaceState(Chicken, walkPerSecond, vegan));
    }

    private void CheckThisObjectPosition()
    {
        globalStatesAndFunctions.CheckPosition(Chicken);
    }
    public IEnumerator AiPeaceState(GameObject target, int walkPerSecond, bool vegan)
    {
        string direction;
        string randomAnimation;
        float animationTime = Random.Range(5, 12);
        int randomNumber = Random.Range(0, 8);

        switch (randomNumber)
        {
            case 0:
                direction = "N";
                break;
            case 1:
                direction = "S";
                break;
            case 2:
                direction = "W";
                break;
            case 3:
                direction = "E";
                break;
            case 4:
                direction = "E";
                break;
            case 5:
                direction = "NE";
                break;
            case 6:
                direction = "NW";
                break;
            case 7:
                direction = "SE";
                break;
            default:
                direction = "SW";
                break;

        }
        randomNumber = Random.Range(0, 100);
        switch (randomNumber)
        {
            case < 55:
                randomAnimation = walking;
                StartCoroutine(animationFunctions.MobGoTo(randomAnimation, idle, target, walkPerSecond, animationTime, direction));
                break;
            case int n when (n >= 55 && n < 60):
                randomAnimation = buff;
                animationTime = 1f;
                animationFunctions.AnimalEventAnimation(randomAnimation, target);
                break;
            case int n when (n >= 60 && n < 75):
                randomAnimation = eat;
                StartCoroutine(animationFunctions.AnimalRestState(randomAnimation, idle, target, animationTime));
                break;
            case int n when (n >= 75 && n < 85):
                randomAnimation = sleep;
                animationTime = Random.Range(12, 20);
                StartCoroutine(animationFunctions.AnimalRestState(randomAnimation, idle, target, animationTime));
                break;
            case < 85:
                if (vegan)
                {
                    randomAnimation = eat;
                    StartCoroutine(animationFunctions.AnimalRestState(randomAnimation, idle, target, animationTime));
                }
                else
                {
                    randomAnimation = walking;
                    StartCoroutine(animationFunctions.MobGoTo(randomAnimation, idle, target, walkPerSecond, animationTime, direction));
                }
                break;
            default:
                randomAnimation = walking;
                StartCoroutine(animationFunctions.MobGoTo(randomAnimation, idle, target, walkPerSecond, animationTime, direction));
                break;
        }
        yield return new WaitForSeconds(animationTime);
        StartCoroutine(AiPeaceState(target, walkPerSecond, vegan));
    }
}
