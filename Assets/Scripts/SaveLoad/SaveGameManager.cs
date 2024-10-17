using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameManager : MonoBehaviour
{
    [SerializeField] private Button saveGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button deleteSaveButton;

    public static SaveData data;

    private void Awake()
    {
        data = new SaveData();
        SaveLoad.OnLoadGame += LoadData;
    }

    private void Start()
    {
        // Podpiêcie przycisków do metod
        saveGameButton.onClick.AddListener(() => SaveData());
        loadGameButton.onClick.AddListener(() => TryLoadGame());
        deleteSaveButton.onClick.AddListener(() => DeleteData());
    }

    public void DeleteData()
    {
        // Usuwanie zapisanych danych
        SaveLoad.DeleteSaveData();
    }

    public static void SaveData()
    {
        // Zapisanie obecnych danych
        SaveLoad.Save(data);
    }

    private void LoadData(SaveData _data)
    {
        // Wczytywanie danych
        data = _data;

        // Odtwarzanie przedmiotów w œwiecie gry
        ItemManager.SpawnItemsFromSave(_data);
    }

    public static void TryLoadGame()
    {
        // Wczytanie gry
        SaveLoad.Load();
    }
}
