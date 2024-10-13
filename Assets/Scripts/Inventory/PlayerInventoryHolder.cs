using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerInventoryHolder : InventoryHolder
{
  
    public static UnityAction OnPlayerInventoryChanged;
    public static PlayerInventoryHolder instance;

    public static UnityAction<InventorySystem, int> OnPlayerInventoryDisplayRequested;
    //protected override void Awake()
    //{

    //    instance = this;
    //}

    private void Start()
    {
        SaveGameManager.data.playerInventory = new InventorySaveData(primaryInventorySystem);
    }
    protected override void LoadInventory(SaveData data)
    {
        if (data.playerInventory.InvSystem != null)
        {
            this.primaryInventorySystem = data.playerInventory.InvSystem; 
            OnPlayerInventoryChanged?.Invoke();
        }
    }

    public void OpenBackpack()
    {
        OnPlayerInventoryDisplayRequested?.Invoke(primaryInventorySystem, offset);  //slots number
    }

    public bool AddToInventory(InventoryItemData data, int ammount)
    {
        if(primaryInventorySystem.AddToInventory(data, ammount))
        {
            return true;
        }
        return false;
    }
}
