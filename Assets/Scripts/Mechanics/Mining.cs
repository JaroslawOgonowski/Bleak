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
        Animator animator = character.GetComponent<Animator>();

        while (true)
        {

            Vector3 direction = miningTarget.transform.position - character.transform.position;
            float currentDistance = direction.magnitude;

            // Check if character is too close or too far
            if (currentDistance > distance + 0.1f)
            {
                // Move closer
                direction.Normalize();
                character.transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }
            else if (currentDistance < distance - 0.1f)
            {
                animator.SetBool("RunForward", true);
                // Move away
                direction.Normalize();
                character.transform.Translate(-direction * moveSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                // Character is at the correct distance
                Debug.Log("Character is at the correct distance from the mining target.");
                yield break;
            }

            yield return null; // Wait until the next frame
        }
    }
}
