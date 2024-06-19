using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slots;
    protected override void Start()
    {
        base.Start();

        if(inventoryHolder != null)
        {
           inventorySystem = inventoryHolder.InventorySystem;
           inventorySystem.OnInventorySlotChanged += UpdatedSlot;
        }
        else
        {
            Debug.LogWarning($"No inventory assigned to {this.gameObject}");
        }
        AssignSlot(inventorySystem);
    }
    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();
        
        if(slots.Length != inventorySystem.InventorySize)
        {
            Debug.LogWarning($"Inventory slots out of sync on {this.gameObject}");
        }
        for (int i = 0; i< inventorySystem.InventorySize; i++)
        {
            SlotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(InventorySystem.InventorySlots[i]);
        }
    }

    
}
