using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    private List<ShopSlot> _shopInventory;
    private int _avaibleGold;
    private float _buyMarkUp;
    private float _sellMarkup;

    public ShopSystem(int size, int gold, float buyMarkUp, float sellMarkUp)
    {
        _avaibleGold = gold;
        _buyMarkUp = buyMarkUp;
        _sellMarkup = sellMarkUp;

        SetShopSize(size);
    }

    private void SetShopSize (int size)
    {

    }
}
