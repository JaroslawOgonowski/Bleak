using UnityEngine;
using System.Collections;
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
    private bool vegan = true;
    AnimationFunctions animationFunctions;
    GlobalStatesAndFunctions globalStatesAndFunctions;
    private void Start()
    {
        animationFunctions = new AnimationFunctions();
        globalStatesAndFunctions = new GlobalStatesAndFunctions();
        InvokeRepeating("CheckThisObjectPosition", 0f, 1f);
        StartCoroutine(AiPeaceState(Bear, walkPerSecond, vegan));
    }

   private void CheckThisObjectPosition()
    {
        globalStatesAndFunctions.CheckPosition(Bear);
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
                randomAnimation = "WalkForward";
                StartCoroutine(animationFunctions.MobGoTo(randomAnimation, "Idle", target, walkPerSecond, animationTime, direction));
                break;
            case int n when (n >= 55 && n < 60):
                randomAnimation = "Buff";
                animationTime = 1f;
                animationFunctions.AnimalEventAnimation(randomAnimation, target);
                break;
            case int n when (n >= 60 && n < 75):
                randomAnimation = "Sit";
                StartCoroutine(animationFunctions.AnimalRestState(randomAnimation, "Idle", target, animationTime));
                break;
            case int n when (n >= 75 && n < 85):
                randomAnimation = "Sleep";
                animationTime = animationTime * 1.5f;
                StartCoroutine(animationFunctions.AnimalRestState(randomAnimation, "Idle", target, animationTime));
                break;
            case < 85:
                if (vegan)
                {
                    randomAnimation = "Eat";
                    StartCoroutine(animationFunctions.AnimalRestState(randomAnimation, "Idle", target, animationTime));
                }
                else
                {
                    randomAnimation = "WalkForward";
                    StartCoroutine(animationFunctions.MobGoTo(randomAnimation, "Idle", target, walkPerSecond, animationTime, direction));
                }
                break;
            default:
                randomAnimation = "WalkForward";
                StartCoroutine(animationFunctions.MobGoTo(randomAnimation, "Idle", target, walkPerSecond, animationTime, direction));
                break;
        }
        yield return new WaitForSeconds(animationTime);
        StartCoroutine(AiPeaceState(target, walkPerSecond, vegan));
    }
}
