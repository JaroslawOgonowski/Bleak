using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemPickUP : MonoBehaviour
{
    public float pickUpRadius = 1f;
    public InventoryItemData itemData;

    private SphereCollider myCollider;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = pickUpRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.transform.GetComponent<InventoryHolder>();

        if(!inventory)return;

        if (inventory.PrimaryInventorySystem.AddToInventory(itemData, 1))
        {
            ResTextManager.instance.ShowText($"{itemData.name} (+{itemData.stackSize})");
            Destroy(this.gameObject);
        }
    }
}
