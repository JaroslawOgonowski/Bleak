using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Funkcja do odtwarzania przedmiot�w na podstawie zapisanych danych
    public static void SpawnItemsFromSave(SaveData saveData)
    {
        foreach (var item in saveData.activeItems)
        {
            var itemData = item.Value.ItemData; // Zmienne itemData
            var position = item.Value.Position;   // Zmienna po�o�enia
            var rotation = item.Value.Rotation;   // Zmienna rotacji

            // U�ywamy prefabrykatu bezpo�rednio
            GameObject prefab = itemData.prefab;  // Pobierz prefabrykat bezpo�rednio z itemData

            if (prefab == null)
            {
                Debug.LogError($"Prefab not found for item: {itemData.displayName}");
                continue; // Kontynuuj, je�li prefabrykat jest null
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
