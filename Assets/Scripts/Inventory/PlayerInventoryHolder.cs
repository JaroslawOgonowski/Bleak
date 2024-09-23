using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] protected int secondaryInventorySize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;

    public InventorySystem SecondaryInventorySystem=> secondaryInventorySystem;
    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;
    public static PlayerInventoryHolder instance;
    protected override void Awake()
    {
        base.Awake();

        secondaryInventorySystem = new InventorySystem(secondaryInventorySize);
        instance = this;
    }
   
    public void OpenBackpack()
    {
        OnPlayerBackpackDisplayRequested?.Invoke(secondaryInventorySystem);
    }
    public bool AddToInventory(InventoryItemData data, int ammount)
    {
        if(primaryInventorySystem.AddToInventory(data, ammount))
        {
            return true;
        }
        else if(secondaryInventorySystem.AddToInventory(data, ammount))
        {
            return true;
        }

        return false;
    }
}
