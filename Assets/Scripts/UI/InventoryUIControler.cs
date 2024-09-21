using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class InventoryUIControler : MonoBehaviour
{
    public DynamicInventoryDisplay chestPanel;
    [SerializeField] private Button closeInvButtonChestPanel;
    public DynamicInventoryDisplay playerBackpack;
    [SerializeField] private Button closeInvButtonPlayerBackpack;

    public static InventoryUIControler instance; 
    private void Awake()
    {
        instance = this;
        chestPanel.gameObject.SetActive(false);
        playerBackpack.gameObject.SetActive(false);
    }

    private void Start()
    {
        closeInvButtonChestPanel.gameObject.SetActive(false);
        closeInvButtonPlayerBackpack.gameObject.SetActive(false);

    }
    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested += DisplayPlayerBackpack;
    }
    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested -= DisplayPlayerBackpack;
    }

    // Update is called once per frame
    void Update()      
    {
    //if (Keyboard.current.bKey.wasPressedThisFrame) DisplayInventory(new InventorySystem(Random.Range(10, 20)));

        if (chestPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            chestPanel.gameObject.SetActive(false);
            closeInvButtonChestPanel.gameObject.SetActive(false);
        }
        if (playerBackpack.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            playerBackpack.gameObject.SetActive(false);
            closeInvButtonPlayerBackpack.gameObject.SetActive(false);
        }

    }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        chestPanel.gameObject.SetActive(true);
        chestPanel.RefreshDynamicInventory(invToDisplay);
        closeInvButtonChestPanel.gameObject.SetActive(true);
    }

   void CloseInventory()
    {
        chestPanel.gameObject.SetActive(false);
        closeInvButtonChestPanel.gameObject.SetActive(false);

    }

    void DisplayPlayerBackpack(InventorySystem invToDisplay)
    {
        playerBackpack.gameObject.SetActive(true);
        playerBackpack.RefreshDynamicInventory(invToDisplay);
        closeInvButtonPlayerBackpack.gameObject.SetActive(true);
    }
}
