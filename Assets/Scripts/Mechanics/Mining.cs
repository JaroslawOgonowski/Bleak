using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mining : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject miningTarget;
    [SerializeField] private Button miningButton;
    public float distance = 5f;
    public float moveSpeed = 2f; // Speed at which the character moves
    public float rotationSpeed = 2f; // Speed at which the character rotates
    private float minimalGatheringDistance = 12f;
    // Start is called before the first frame update
    void Start()
    {
        miningButton.onClick.AddListener(() => onMiningButtonClick());
    }

    void onMiningButtonClick()
    {
        StartCoroutine(MoveCharacterToTarget());
    }

    IEnumerator MoveCharacterToTarget()
    {

        while (true)
        {
            ExamplePlayerController.Instance.gatheringMove = true;
            Vector3 direction = miningTarget.transform.position - character.transform.position;
            float currentDistance = direction.magnitude;

            // Rotate character to face the mining target
            direction.y = 0; // Keep only the horizontal direction
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Check if character is too close or too far
            if (currentDistance > distance + 0.1f)
            {
                ExamplePlayerController.Instance.animator.SetBool("RunForward", true);
                ExamplePlayerController.Instance.animator.SetBool("RunBackward", false);
                // Move closer
                direction.Normalize();
                character.transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }
            else if (currentDistance < distance - 0.1f)
            {
                ExamplePlayerController.Instance.animator.SetBool("RunBackward", true);
                ExamplePlayerController.Instance.animator.SetBool("RunForward", false);
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
                    // Character is at the correct distance and properly oriented
                    Debug.Log("Character is at the correct distance from the mining target.");
                    ExamplePlayerController.Instance.animator.SetBool("RunBackward", false);
                    ExamplePlayerController.Instance.animator.SetBool("RunForward", false);
                    ExamplePlayerController.Instance.gatheringMove = false;
                    ExamplePlayerController.Instance.animator.SetBool("Mining", true);
                    yield break;
                }
            }

            yield return null; // Wait until the next frame
        }
    }

}
