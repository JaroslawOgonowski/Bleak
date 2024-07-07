using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChestInventory : InventoryHolder, IInteractable
{

    UnityAction<IInteractable> IInteractable.OnInteractionComplete { get;  set; }


    void IInteractable.Interact(Interactor interactor, out bool interactSuccessful)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(inventorySystem);
        interactSuccessful = true;
    }


    void IInteractable.EndInteraction()
    {
        
    }
}
