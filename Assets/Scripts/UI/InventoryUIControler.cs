using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class InventoryUIControler : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;
    [SerializeField] private Button closeInvButtonChestPanel;
    public DynamicInventoryDisplay playerBackpack;
    [SerializeField] private Button closeInvButtonPlayerBackpack;
    [SerializeField] private Button backpackButton;
    public static InventoryUIControler instance;
    [SerializeField] PlayerInventoryHolder playerInventoryHolder;
    private void Awake()
    {
        instance = this;
        inventoryPanel.gameObject.SetActive(false);
        playerBackpack.gameObject.SetActive(false);
    }

    private void Start()
    {
        closeInvButtonChestPanel.gameObject.SetActive(false);
        closeInvButtonPlayerBackpack.gameObject.SetActive(false);
        backpackButton.onClick.AddListener(() => BackpackCheck());
    }
    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += DisplayPlayerInventory;
    }
    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= DisplayPlayerInventory;
    }

    private void BackpackCheck()
    {
        if (playerBackpack.gameObject.activeInHierarchy)
        {
            playerBackpack.gameObject.SetActive(false);
            closeInvButtonPlayerBackpack.gameObject.SetActive(false);
        }
        else
        {
            playerInventoryHolder.OpenBackpack();            
        }
    }
    // Update is called once per frame
    void Update()      
    {
    //if (Keyboard.current.bKey.wasPressedThisFrame) DisplayInventory(new InventorySystem(Random.Range(10, 20)));

        if (inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            inventoryPanel.gameObject.SetActive(false);
            closeInvButtonChestPanel.gameObject.SetActive(false);
        }
        if (playerBackpack.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            playerBackpack.gameObject.SetActive(false);
            closeInvButtonPlayerBackpack.gameObject.SetActive(false);

        }
    }

    void DisplayInventory(InventorySystem invToDisplay, int offset)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay, offset);
        closeInvButtonChestPanel.gameObject.SetActive(true);
    }
    void DisplayPlayerInventory(InventorySystem invToDisplay, int offset)        //backpack
    {
        playerBackpack.gameObject.SetActive(false);
        playerBackpack.gameObject.SetActive(true);
        playerBackpack.RefreshDynamicInventory(invToDisplay, offset);
        closeInvButtonPlayerBackpack.gameObject.SetActive(true);
    }
    public void CloseBackpack()
    {
        playerBackpack.gameObject.SetActive(false);
        closeInvButtonPlayerBackpack.gameObject.SetActive(false);

    }


}
