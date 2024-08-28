using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class InventoryUIControler : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;

    public static InventoryUIControler instance; 
    private void Awake()
    {
        instance = this;
        inventoryPanel.gameObject.SetActive(false);
    }

    private void Start()
    {
    }
    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
    }
    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;

    }

    // Update is called once per frame
    void Update()      
    {
    //if (Keyboard.current.bKey.wasPressedThisFrame) DisplayInventory(new InventorySystem(Random.Range(10, 20)));

        if (inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            inventoryPanel.gameObject.SetActive(false);
    }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay); 
    }

   void CloseInventory()
    {
        inventoryPanel.gameObject.SetActive(false);

    }
}
