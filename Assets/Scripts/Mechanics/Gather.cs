using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum GatherSkillList
{
    Mining,
    Lumber,
    Harvest,
    Picklock,

}

public class Gather : MonoBehaviour
{
    public static Gather instance;
    [SerializeField] private GameObject character;
    public float minimalDistance = 12f;
    public float distance = 5f;
    public float moveSpeed = 2f; // Prêdkoœæ poruszania siê postaci
    public float rotationSpeed = 2f; // Prêdkoœæ obracania siê postaci
    private float minimalGatheringDistance = 12f;
    private Animator animator;
    public GameObject currentTarget;
    public bool gatherProcess = false;
    private CharacterController characterController;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator = character.GetComponent<Animator>();
        characterController = character.GetComponent<CharacterController>();
    }

    public void GatherByType(GameObject target, GatherSkillList type)
    {
        CheckDistance(target, type); 
    }


    private void CheckDistance(GameObject target, GatherSkillList animationTrigger)
    {
        float currentDistance = Vector3.Distance(target.transform.position, character.transform.position);
        if (currentDistance < minimalDistance)
        {
            StartCoroutine(MoveCharacterToTarget(target, animationTrigger));
        }
        else
        {
            StartCoroutine(GatheringPanelManager.instance.FarAwayPanelOpen());
        }
    }
    IEnumerator MoveCharacterToTarget(GameObject target, GatherSkillList animationTrigger)
    {
        GatheringPanelManager.instance.gatheringPanel.SetActive(false);
        if (!gatherProcess)
        {
            gatherProcess = true;
            CharacterMotion.instance.gatheringMove = true;
            while (true)
            {
                Vector3 direction = target.transform.position - character.transform.position;
                float currentDistance = direction.magnitude;

                // Obróæ postaæ w kierunku celu
                direction.y = 0; // Uwzglêdniaj tylko kierunek poziomy
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // SprawdŸ odleg³oœæ od celu

                if (currentDistance > distance + 0.1f)
                {
                    Debug.Log("Poruszanie siê bli¿ej celu");
                    animator.SetBool("RunBackward", false);
                    animator.SetBool("RunForward", true);
                    // Poruszaj siê bli¿ej
                    direction.Normalize();
                    characterController.Move(direction * moveSpeed * Time.deltaTime);
                }
                else if (currentDistance < distance - 0.1f)
                {
                    Debug.Log("Poruszanie siê dalej od celu");
                    animator.SetBool("RunForward", false);
                    animator.SetBool("RunBackward", true);
                    // Poruszaj siê dalej
                    direction.Normalize();
                    characterController.Move(-direction * moveSpeed * Time.deltaTime);
                }
                else
                {
                    // SprawdŸ, czy postaæ jest odpowiednio zorientowana w kierunku celu
                    float angle = Quaternion.Angle(character.transform.rotation, targetRotation);
                    if (angle < 5f) // Dostosuj próg k¹ta w razie potrzeby
                    {
                        // Postaæ jest w odpowiedniej odleg³oœci i odpowiednio zorientowana
                        Debug.Log("Postaæ jest w odpowiedniej odleg³oœci od celu.");
                        animator.SetBool("RunBackward", false);
                        animator.SetBool("RunForward", false);
                        string aT = animationTrigger.ToString();
                        if (aT == "Lumber")
                        {
                            direction.y = 10; // Uwzglêdniaj tylko kierunek poziomy
                            Quaternion pos = Quaternion.LookRotation(direction);
                            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                        }
                        animator.SetBool(aT, true);
                        currentTarget = target;
                        yield break;
                    }
                }

                yield return null;
            }
        }
    }

    public void OnAnimationEvent()
    {
        int rn = Random.Range(1, 3);
        ResTextManager.instance.ShowText($"{currentTarget.GetComponent<GatheringObject>().name} (+{rn})");
        GatheringPanelManager.instance.currentTarget = null;
        Destroy(currentTarget);
        currentTarget = null;
        gatherProcess = false;
        CharacterMotion.instance.gatheringMove = false;
    }
}