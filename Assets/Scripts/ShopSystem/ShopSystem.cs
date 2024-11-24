using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ShopSystem
{
    [SerializeField] private List<ShopSlot> _shopInventory;
    [SerializeField] private int _avaibleGold;
    [SerializeField] private float _buyMarkUp;
    [SerializeField] private float _sellMarkup;

    public int AvaibleGold => _avaibleGold;
    public ShopSystem(int size, int gold, float buyMarkUp, float sellMarkUp)
    {
        _avaibleGold = gold;
        _buyMarkUp = buyMarkUp;
        _sellMarkup = sellMarkUp;

        SetShopSize(size);
    }

    private void SetShopSize (int size)
    {
       _shopInventory = new List<ShopSlot>(size);
        for(int i = 0; i < size; i++)
        {
            _shopInventory.Add(new ShopSlot());
        }
    }

    public void AddToShop(InventoryItemData data, int ammount)
    {
         if(ContainsItem(data, out ShopSlot shopSlot))
        {
            shopSlot.AddToStack(ammount);
        }

        var freeSlot = GetFreeSlot();
        freeSlot.AssignItem(data, ammount);
    }

    private ShopSlot GetFreeSlot()
    {
        var freeSlot = _shopInventory.FirstOrDefault(i=>i.ItemData == null);

        if(freeSlot == null)
        {
            freeSlot = new ShopSlot();
            _shopInventory.Add(freeSlot);
        }

        return freeSlot;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out ShopSlot shopSlot)
    {
        shopSlot = _shopInventory.Find(i=>i.ItemData == itemToAdd);
        return shopSlot != null;
    }
}
