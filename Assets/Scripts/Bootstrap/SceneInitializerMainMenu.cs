using UnityEngine;

public class SceneInitializerMainMenu : MonoBehaviour
{
    private void Awake()
    {
        bool existingSaveData = SaveSystem.TryLoad(out SaveData saveData);
        if (!existingSaveData)
        {
            return;
        }

        GameObject dataTransportObject = new GameObject();
        dataTransportObject.name = "Loaded Save Data";
        LoadedSaveData loadedSaveData = dataTransportObject.AddComponent<LoadedSaveData>();
        loadedSaveData.SaveData = saveData;
    }
}
