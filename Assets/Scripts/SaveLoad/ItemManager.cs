using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    
    public static void SpawnItemsFromSave(SaveData saveData)
    {
        foreach (var item in saveData.activeItems)
        {
            var itemData = item.Value.ItemData; 
            var position = item.Value.Position;  
            var rotation = item.Value.Rotation;  

    
            GameObject prefab = itemData.prefab;

            if (prefab == null)
            {
                Debug.LogError($"Prefab not found for item: {itemData.displayName}");
                continue; 
            }

    
            GameObject spawnedItem = Instantiate(prefab, position, rotation);
            var itemPickUp = spawnedItem.GetComponent<ItemPickUP>();

     
            itemPickUp.itemData = itemData;
            itemPickUp.SetID(item.Key);
        }
    }
}
