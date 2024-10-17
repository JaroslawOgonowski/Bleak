using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(UniqueID))]
public class ItemPickUP : MonoBehaviour
{
    public float pickUpRadius = 1f;
    public InventoryItemData itemData;
    [SerializeField] private float _rotationSpeed = 20;
    private SphereCollider myCollider;
    private string id;

    [SerializeField] private ItemPickUpSaveData itemSaveData;

    private void Awake()
    {
        // Subskrybowanie eventów ³adowania gry
        SaveLoad.OnLoadGame += LoadGame;
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = pickUpRadius;
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(id))
        {
            id = GetComponent<UniqueID>().ID;
        }
        SaveItem();
    }

    private void SaveItem()
    {
        itemSaveData = new ItemPickUpSaveData(itemData, transform.position, transform.rotation);

        // Dodanie przedmiotu do activeItems tylko jeœli go tam nie ma
        if (!SaveGameManager.data.activeItems.ContainsKey(id))
        {
            SaveGameManager.data.activeItems.Add(id, itemSaveData);
        }
    }

    private void LoadGame(SaveData data)
    {
        // Jeœli przedmiot zosta³ podniesiony, usuñ go
        if (data.collectedItems.Contains(id))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        // Odsubskrybowanie eventów i usuniêcie przedmiotu z aktywnych po zniszczeniu
        SaveLoad.OnLoadGame -= LoadGame;
        if (SaveGameManager.data.activeItems.ContainsKey(id))
        {
            SaveGameManager.data.activeItems.Remove(id);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();

        if (!inventory) return;

        if (inventory.AddToInventory(itemData, 1))
        {
            // Komunikat o zebraniu przedmiotu
            ResTextManager.instance.ShowText($"{itemData.name} (+{itemData.stackSize})");

            // Dodanie przedmiotu do collectedItems
            SaveGameManager.data.collectedItems.Add(id);
            Destroy(this.gameObject);
        }
    }

    // Ustawienie ID przedmiotu po wczytaniu go
    public void SetID(string newID)
    {
        id = newID;
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