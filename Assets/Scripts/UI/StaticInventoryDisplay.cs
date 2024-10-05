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
           inventorySystem = inventoryHolder.PrimaryInventorySystem;
           inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else
        {
            Debug.LogWarning($"No inventory assigned to {this.gameObject}");
        }
        AssignSlot(inventorySystem, 0);
    }
    public override void AssignSlot(InventorySystem invToDisplay, int offset)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();
        
        for (int i = 0; i< inventoryHolder.Offset; i++)
        {
            SlotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(InventorySystem.InventorySlots[i]);
        }
    }

    
}
