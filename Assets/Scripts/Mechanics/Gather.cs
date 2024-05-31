using System.Collections;
using UnityEngine;

public class Gather : MonoBehaviour
{
    public static Gather instance;
    [SerializeField] private GameObject character;
    public float distance = 5f;
    public float moveSpeed = 2f; // Pr�dko�� poruszania si� postaci
    public float rotationSpeed = 2f; // Pr�dko�� obracania si� postaci
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

    public void onMiningButtonClick(GameObject target)
    {
        StartCoroutine(MoveCharacterToTarget(target, "Mining"));
    }

    public void onLumberButtonClick(GameObject target)
    {
        StartCoroutine(MoveCharacterToTarget(target, "Lumber"));
    }

    IEnumerator MoveCharacterToTarget(GameObject target, string animationTrigger)
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

                // Obr�� posta� w kierunku celu
                direction.y = 0; // Uwzgl�dniaj tylko kierunek poziomy
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // Sprawd� odleg�o�� od celu
                if (currentDistance > distance + 0.1f)
                {
                    Debug.Log("Poruszanie si� bli�ej celu");
                    animator.SetBool("RunBackward", false);
                    animator.SetBool("RunForward", true);
                    // Poruszaj si� bli�ej
                    direction.Normalize();
                    characterController.Move(direction * moveSpeed * Time.deltaTime);
                }
                else if (currentDistance < distance - 0.1f)
                {
                    Debug.Log("Poruszanie si� dalej od celu");
                    animator.SetBool("RunForward", false);
                    animator.SetBool("RunBackward", true);
                    // Poruszaj si� dalej
                    direction.Normalize();
                    characterController.Move(-direction * moveSpeed * Time.deltaTime);
                }
                else
                {
                    // Sprawd�, czy posta� jest odpowiednio zorientowana w kierunku celu
                    float angle = Quaternion.Angle(character.transform.rotation, targetRotation);
                    if (angle < 5f) // Dostosuj pr�g k�ta w razie potrzeby
                    {
                        // Posta� jest w odpowiedniej odleg�o�ci i odpowiednio zorientowana
                        Debug.Log("Posta� jest w odpowiedniej odleg�o�ci od celu.");
                        animator.SetBool("RunBackward", false);
                        animator.SetBool("RunForward", false);
                        if (animationTrigger == "Lumber")
                        {
                            direction.y = 10; // Uwzgl�dniaj tylko kierunek poziomy
                            Quaternion pos = Quaternion.LookRotation(direction);
                            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                        }
                        animator.SetBool(animationTrigger, true);
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