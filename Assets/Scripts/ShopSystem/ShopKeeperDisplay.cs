using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ShopKeeperDisplay : MonoBehaviour
{
    [SerializeField] private ShopSlotUI _shopSlotPrefab;
    [SerializeField] private ShopingCartItemUI _shopingCartItemPrefab;

    [SerializeField] private Button _buyTab;
    [SerializeField] private Button _sellTab;
    [SerializeField] private Button _closeShopWindow;

    [Header("Shopping cart")]
    [SerializeField] private TextMeshProUGUI _basketTotalText;
    [SerializeField] private TextMeshProUGUI _playerGoldText;
    [SerializeField] private TextMeshProUGUI _shopGoldText;
    [SerializeField] private TextMeshProUGUI _buyButtonText;
    [SerializeField] private Button _buyButton;

    [Header("Item previev section")]
    [SerializeField] Image _itemPreviewSprite;
    [SerializeField] private TextMeshProUGUI _itemPreviewName;
    [SerializeField] private TextMeshProUGUI _itemPreviewDescription;

    [SerializeField] private GameObject _itemListContentPanel;
    [SerializeField] private GameObject _shoppingCartContentPanel;

    [SerializeField] private UIController _uiController;

    private int _basketTotal;

    private ShopSystem _shopSystem;
    private PlayerInventoryHolder _playerInventoryHolder;

    private Dictionary<InventoryItemData, int> _shoppingCart = new Dictionary<InventoryItemData, int>();
    private Dictionary<InventoryItemData, ShopingCartItemUI> _shoppingCartUI = new Dictionary<InventoryItemData, ShopingCartItemUI>();

    public void DisplayShopWindow(ShopSystem shopSystem, PlayerInventoryHolder playerInventoryHolder)
    {
        _shopSystem = shopSystem;
        _playerInventoryHolder = playerInventoryHolder;

        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        ClearSlots();

        _basketTotalText.enabled = false;
        _buyButton.gameObject.SetActive(false);
        _basketTotal = 0;
        _playerGoldText.text = _playerInventoryHolder.PrimaryInventorySystem.Gold.ToString();
        _shopGoldText.text = _shopSystem.AvaibleGold.ToString();
    }

    private void ClearSlots()
    {
        _shoppingCart = new Dictionary<InventoryItemData, int>();
        _shoppingCartUI = new Dictionary<InventoryItemData, ShopingCartItemUI>();
        foreach(var item in _itemListContentPanel.transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }
    }
    private void Start()
    {
        _closeShopWindow.onClick.AddListener(() => _uiController.CloseShopWindow());
    }
}
