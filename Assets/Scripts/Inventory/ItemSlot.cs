using System;
using UnityEngine;

public abstract class ItemSlot : ISerializationCallbackReceiver
{
    [NonSerialized] protected InventoryItemData itemData;
    [SerializeField] protected int _itemID = -1;
    [SerializeField] protected int stackSize;

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public void ClearSlot()
    {
        itemData = null;
        _itemID = -1;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot)
    {
        if (itemData == invSlot.itemData) AddToStack(invSlot.stackSize);
        else
        {
            itemData = invSlot.itemData;
            _itemID = itemData.ID;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public void OnAfterDeserialize()
    {
        if (_itemID == -1) return;

        var db = Resources.Load<DatabaseItems>("ItemDatabase");
        itemData = db.GetItem(_itemID);
    }

    public void OnBeforeSerialize()
    {
    }
}