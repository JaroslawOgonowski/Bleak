using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mining : MonoBehaviour
{
    public static Mining instance;
    [SerializeField] private GameObject character;
    public float distance = 5f;
    public float moveSpeed = 2f; // Speed at which the character moves
    public float rotationSpeed = 2f; // Speed at which the character rotates
    private float minimalGatheringDistance = 12f;
    private Animator animator;
    public GameObject currentTarget;
    private bool miningProcess = false;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator = ExamplePlayerController.Instance.animator;
    }

    public void onMiningButtonClick(GameObject target)
    {
        StartCoroutine(MoveCharacterToTarget(target));
    }

    IEnumerator MoveCharacterToTarget(GameObject target)
    {
        GatheringPanelManager.instance.gatheringPanel.SetActive(false);
        if (!miningProcess)
        {
            while (true)
            {
                miningProcess = true;
                ExamplePlayerController.Instance.gatheringMove = true;
                Vector3 direction = target.transform.position - character.transform.position;
                float currentDistance = direction.magnitude;

                // Rotate character to face the mining target
                direction.y = 0; // Keep only the horizontal direction
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // Check if character is too close or too far
                if (currentDistance > distance + 0.1f)
                {
                    animator.SetBool("RunForward", true);
                    animator.SetBool("RunBackward", false);
                    // Move closer
                    direction.Normalize();
                    character.transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
                }
                else if (currentDistance < distance - 0.1f)
                {
                    animator.SetBool("RunBackward", true);
                    animator.SetBool("RunForward", false);
                    // Move away
                    direction.Normalize();
                    character.transform.Translate(-direction * moveSpeed * Time.deltaTime, Space.World);
                }
                else
                {
                    // Check if character is properly oriented towards the target
                    float angle = Quaternion.Angle(character.transform.rotation, targetRotation);
                    if (angle < 5f) // Adjust the threshold angle as needed
                    {
                        CharacterFreezePosition(character);
                        // Character is at the correct distance and properly oriented
                        Debug.Log("Character is at the correct distance from the mining target.");
                        animator.SetBool("RunBackward", false);
                        animator.SetBool("RunForward", false);
                        animator.SetBool("Mining", true);
                        currentTarget = target;
                        yield break;
                    }
                }

                yield return null;
            }
        }
       
    }

    private void CharacterFreezePosition(GameObject character)
    {
        Rigidbody rb = character.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void CharacterUnfreezePosition(GameObject character)
    {
        Rigidbody rb = character.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        }
    }

    public void OnAnimationEvent()
    {
        
        GatheringPanelManager.instance.currentTarget = null;
        Destroy(currentTarget);
        currentTarget = null;
        miningProcess = false;
        CharacterUnfreezePosition(character);
        ExamplePlayerController.Instance.gatheringMove = false;

    }
}
