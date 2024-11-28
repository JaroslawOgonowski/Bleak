using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class ShopSlotUI : MonoBehaviour
{

    [SerializeField] private Image _itemSprite;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemCount;
    [SerializeField] private ShopSlot _assignedItemSlot;
    [SerializeField] private TextMeshProUGUI _goldValue;


    [SerializeField] private Button _addItemToCartButton;
    [SerializeField] private Button _removeItemFromCartButton;

    public ShopKeeperDisplay ParentDisplay { get; private set; }
    public float MarkUp {  get; private set; }
    private void Awake()
    {
        _itemSprite.sprite = null;
        _itemSprite.preserveAspect = true;
        _itemSprite.color = Color.clear;
        _itemName.text = "";
        _itemCount.text = "";
        _goldValue.text = "";
       _addItemToCartButton?.onClick.AddListener(AddItemToCart);
        _removeItemFromCartButton?.onClick.AddListener(RemoveItemFromCart);
        ParentDisplay = transform.parent.GetComponentInParent<ShopKeeperDisplay>();
    }

    private void RemoveItemFromCart()
    {
        Debug.Log("RemoveItemFromCart");
    }

    private void AddItemToCart()
    {
        Debug.Log("AddItemToCart");
    }

    public void Init(ShopSlot slot, float markUp)
    {
      _assignedItemSlot = slot;
        MarkUp = markUp;
        UpdateUISlot();
    }

    private void UpdateUISlot()
    {
          if(_assignedItemSlot.ItemData !=null)
        {
            if (_assignedItemSlot.ItemData.icon != null)
            {
                _itemSprite.sprite = _assignedItemSlot.ItemData.icon;
            }                                               
            _itemSprite.color = Color.white;
            _itemCount.text = $"x{_assignedItemSlot.StackSize.ToString()}";
            _itemName.text = $"{_assignedItemSlot.ItemData.displayName}";
            _goldValue.text = $"{_assignedItemSlot.ItemData.GoldValue}";
        }  
        else
        {
            _itemSprite = null;
            _itemSprite.color = Color.clear;
            _itemName.text = "";
            _itemCount.text = "";
            _goldValue.text = "";
        }
    }
}
