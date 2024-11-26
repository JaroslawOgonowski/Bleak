using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopSlotUI : MonoBehaviour
{

    [SerializeField] private Image _itemSprite;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemCount;
    [SerializeField] private ShopSlot _assignedItemSlot;


    [SerializeField] private Button _addItemToCartButton;
    [SerializeField] private Button _removeItemFromCartButton;

    public ShopKeeperDisplay ParentDisplay { get; private set; }
    private void Awake()
    {
        _itemSprite = null;
        _itemSprite.preserveAspect = true;
        _itemSprite.color = Color.clear;
        _itemName.text = "";
        _itemCount.text = "";

        _addItemToCartButton?.onClick.AddListener(AddItemToCart);
        _removeItemFromCartButton?.onClick.AddListener(RemoveItemFromCart);
        ParentDisplay= transform.parent.GetComponentInParent<ShopKeeperDisplay>();
    }

    private void RemoveItemFromCart()
    {
        throw new NotImplementedException();
    }

    private void AddItemToCart()
    {
        throw new NotImplementedException();
    }

    public void Init(ShopSlot slot, float markUp)
    {

    }
}
