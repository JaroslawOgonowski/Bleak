using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(UniqueID))]
public class ItemPickUP : MonoBehaviour
{
    public float pickUpRadius = 1f;
    public InventoryItemData itemData;

    private SphereCollider myCollider;

    [SerializeField] private ItemPickUpSaveData itemSaveData;
    private string id;
    private void Awake()
    {
        id = GetComponent<UniqueID>().ID;
        SaveLoad.OnLoadGame += LoadGame;
        itemSaveData = new ItemPickUpSaveData(itemData, transform.position, transform.rotation);

        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = pickUpRadius;
    }
    private void Start()
    {
        SaveGameManager.data.activeItems.Add(id, itemSaveData);
    }

    private void LoadGame(SaveData data)
    {
        if(data.collectedItems.Contains(id)) Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if(SaveGameManager.data.activeItems.ContainsKey(id)) SaveGameManager.data.activeItems.Remove(id);
        SaveLoad.OnLoadGame -= LoadGame;
    }
  
    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();

        if(!inventory)return;

        if (inventory.AddToInventory(itemData, 1))
        {

            ResTextManager.instance.ShowText($"{itemData.name} (+{itemData.stackSize})");
            SaveGameManager.data.collectedItems.Add(id);
            Destroy(this.gameObject);
        }
    }
}

[System.Serializable]
public struct ItemPickUpSaveData
{
    public InventoryItemData ItemData;
    public Vector3 Position;
    public Quaternion Rotation;

    public ItemPickUpSaveData(InventoryItemData _itemData, Vector3 _position, Quaternion _rotation)
    {
        ItemData = _itemData;
        Position = _position;
        Rotation = _rotation;
    }
}