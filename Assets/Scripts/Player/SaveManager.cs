using System;
using System.Net;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerSaveData
{
    public Inventory inventoryManager;
}
    
public class SaveManager : MonoBehaviour
{
    private static string path;

    private void Awake()
    {
        path = Application.persistentDataPath + "/PlayerData.json";
    }

    public static void Save(PlayerSaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("Player Data Saved: " + path);
    }

    public static PlayerSaveData Load()
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("[Save Data] File does not exist while loading. Creating new file.");
            Save(new PlayerSaveData());
        }
        
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<PlayerSaveData>(json);
    }
}
