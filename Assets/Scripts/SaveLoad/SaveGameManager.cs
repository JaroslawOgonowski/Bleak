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
        saveGameButton.onClick.AddListener(() => SaveData());
        loadGameButton.onClick.AddListener(() => TryLoadGame());
        deleteSaveButton.onClick.AddListener(() => DeleteData());
    }

    public void DeleteData()
    {
        SaveLoad.DeleteSaveData();
    }

    public static void SaveData()
    {
        SaveLoad.Save(data);
    }

    private void LoadData(SaveData _data)
    {
        data = _data;
        ItemManager.SpawnItemsFromSave(_data);
    }

    public static void TryLoadGame()
    {
        SaveLoad.Load();
    }
}
