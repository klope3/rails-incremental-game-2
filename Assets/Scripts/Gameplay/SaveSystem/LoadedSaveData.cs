using UnityEngine;

//simple component used to transport loaded save data (if any) into other scenes from a starting scene, such as a main menu, where that data was loaded.
//other components can reach into this one to read the loaded data and apply it to various objects in the scene as needed
public class LoadedSaveData : MonoBehaviour
{
    public SaveData SaveData { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
