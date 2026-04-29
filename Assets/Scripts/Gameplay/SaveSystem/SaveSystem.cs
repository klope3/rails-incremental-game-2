using System;
using System.IO;
using UnityEngine;

//core of the save system, handling the I/O operations. Currently very simple but should be more robust in the future.
public static class SaveSystem
{
    //expects the SaveData to already be created from elsewhere, since the data is so game-specific.
    public static void Save(SaveData saveData)
    {
        saveData.metaData.version = Application.version;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText($"{Application.persistentDataPath}/save.txt", json);
        Debug.Log("Data saved");
    }

    public static bool TryLoad(out SaveData saveData)
    {
        try
        {
            string json = File.ReadAllText($"{Application.persistentDataPath}/save.txt");
            saveData = JsonUtility.FromJson<SaveData>(json);
            return true;
        }
        catch (Exception)
        {
            saveData = null;
            return false;
        }
    }
}
