using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactRange = 3f;
    [SerializeField] GatheringPanelManager gatheringManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach(Collider collider in colliderArray)
            {
                if(collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact();
                }
                if(collider.TryGetComponent(out GatheringObject gatheringObject))
                {
                    gatheringManager.OpenGatheringPanel(gatheringObject.gameObject);
                }
            }
        }
       
    }

    public NPCInteractable GetInteractableObject()
    {
        List<NPCInteractable> interactables = new List<NPCInteractable>();

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractable npcInteractable))
            {
                interactables.Add(npcInteractable);
            }
        }
        NPCInteractable closestNPCInteractable=null;
        foreach (NPCInteractable npcInteractable in interactables)
        {
            if(closestNPCInteractable == null)
            {
                closestNPCInteractable=npcInteractable;
            } else
            {
                if(Vector3.Distance(transform.position, npcInteractable.transform.position) < Vector3.Distance(transform.position, closestNPCInteractable.transform.position))
                {
                    closestNPCInteractable = npcInteractable;
                }
            }
        }

        return closestNPCInteractable;

    }
}
