using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Funkcja do odtwarzania przedmiotów na podstawie zapisanych danych
    public static void SpawnItemsFromSave(SaveData saveData)
    {
        foreach (var item in saveData.activeItems)
        {
            var itemData = item.Value.ItemData; // Zmienne itemData
            var position = item.Value.Position;   // Zmienna po³o¿enia
            var rotation = item.Value.Rotation;   // Zmienna rotacji

            // U¿ywamy prefabrykatu bezpoœrednio
            GameObject prefab = itemData.prefab;  // Pobierz prefabrykat bezpoœrednio z itemData

            if (prefab == null)
            {
                Debug.LogError($"Prefab not found for item: {itemData.displayName}");
                continue; // Kontynuuj, jeœli prefabrykat jest null
            }

            // Tworzenie przedmiotu na podstawie zapisanych danych
            GameObject spawnedItem = Instantiate(prefab, position, rotation);
            var itemPickUp = spawnedItem.GetComponent<ItemPickUP>();

            // Ustawienie danych dla ItemPickUP
            itemPickUp.itemData = itemData;
            itemPickUp.SetID(item.Key); // Przypisujemy ID
        }
    }
}
