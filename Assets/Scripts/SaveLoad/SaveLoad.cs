using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;
using Directory = System.IO.Directory;
using File = System.IO.File;

public static class SaveLoad
{
    public static UnityAction OnSaveGame;
    public static UnityAction<SaveData> OnLoadGame;

    public static string directory = "/SaveData/";
    public static string fileName = "SaveGame.sav";

    public static bool Save(SaveData data)
    {
        OnSaveGame?.Invoke();

        string dir = Application.persistentDataPath + directory;

        if(!Directory.Exists(dir))
        {
           Directory.CreateDirectory(dir); 
        }
        string json = JsonUtility.ToJson(data, true);
        
        File.WriteAllText(dir, json);


        Debug.Log("SaveGame");

        return true;
        
    }

    public static SaveData Load()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;
        SaveData data = new SaveData();
        
        if(File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            data = JsonUtility.FromJson<SaveData>(json);

            OnLoadGame?.Invoke(data);

        }
        else
        {
            Debug.Log("Save File does not exist!");
        }
        return data;
    }

}
