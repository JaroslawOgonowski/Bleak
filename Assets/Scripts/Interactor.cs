using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public Transform InteractionPoint;
    public LayerMask InteractionLayer;
    public float InteractionPointRadius = 1f;
    public Button actionButton;
    public bool IsInteracting { get; private set; }
    public static Interactor Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void InteractionSearch()
    {
        var colliders = Physics.OverlapSphere(InteractionPoint.position, InteractionPointRadius, InteractionLayer);
        IInteractable currentInteractable = null;

            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();

                if (interactable != null)
                {
                    Debug.Log("Interacting with: " + colliders[i].name);
                    currentInteractable = interactable;
                    StartInteraction(interactable);
                }
            }
        
        bool stillInRange = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<IInteractable>() == currentInteractable)
            {
                stillInRange = true;
                break;
            }
        }

        // If the player is no longer in range of the current interactable, end the interaction
        if (!stillInRange && currentInteractable != null)
        {
            EndInteraction();
            currentInteractable = null;
        }
    }

    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
        IsInteracting = true;
    }

    void EndInteraction()
    {
        IsInteracting = false;

    }
}
