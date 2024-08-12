using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactRange = 3f;
    [SerializeField] Button actionButton;
    [SerializeField] GameObject interactor;
    private IClickInteract currentInteractor;

    private void Start()
    {
        actionButton.onClick.AddListener(() => currentInteractor.Interact(transform));
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        IClickInteract interactable = GetInteractableObject();
    //        if(interactable != null)
    //        {
    //            interactable.Interact(transform);
    //        }
    //    }
       
    //}

    public IClickInteract GetInteractableObject()
    {
        List<IClickInteract> interactables = new List<IClickInteract>();

        Collider[] colliderArray = Physics.OverlapSphere(interactor.transform.position, interactRange);
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
                if(Vector3.Distance(interactor.transform.position, interactable.GetTransform().position) < Vector3.Distance(interactor.transform.position, interactable.GetTransform().position))
                {
                    closestInteractable = interactable;
                }
            }
        }

        currentInteractor = closestInteractable;
        return closestInteractable;

    }
}
