using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(UniqueID))]
public class ShopKeeper : MonoBehaviour, IInteractable
{
    [SerializeField] private ShopItemList _shopItemsHeld;
    [SerializeField] private ShopSystem _shopSystem;
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    public static UnityAction<ShopSystem, PlayerInventoryHolder> OnShopWindowRequested;
    private void Awake()
    {
        _shopSystem = new ShopSystem(_shopItemsHeld.Items.Count,
            _shopItemsHeld.MaxAllowedGold, _shopItemsHeld.BuyMarkUp, _shopItemsHeld.SellMarkUp);

        foreach(var item in _shopItemsHeld.Items)
        {
            Debug.Log($"{ item.ItemData.displayName}: { item.Amount}");
            _shopSystem.AddToShop(item.ItemData, item.Amount);
        }
    }
    public void EndInteraction()
    {

    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        var playerInv = interactor.GetComponent<PlayerInventoryHolder>();
        
        if(playerInv != null)
        {
           OnShopWindowRequested?.Invoke(_shopSystem, playerInv);
            interactSuccessful = true;
        }
        else
        {
            interactSuccessful = false;
            Debug.LogError("Player inv not found");
        }
    }
}
