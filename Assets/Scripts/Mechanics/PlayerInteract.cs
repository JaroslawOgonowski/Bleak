using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactRange = 3f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IClickInteract interactable = GetInteractableObject();
            if(interactable != null)
            {
                interactable.Interact(transform);
            }
        }
       
    }

    public IClickInteract GetInteractableObject()
    {
        List<IClickInteract> interactables = new List<IClickInteract>();

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out IClickInteract interactable))
            {
                interactables.Add(interactable);
                Debug.Log(interactable);
            }
        }

        IClickInteract closestInteractable =null;
        foreach (IClickInteract interactable in interactables)
        {
            if(closestInteractable == null)
            {
                closestInteractable = interactable;
            } else
            {
                if(Vector3.Distance(transform.position, interactable.GetTransform().position) < Vector3.Distance(transform.position, interactable.GetTransform().position))
                {
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;

    }
}
