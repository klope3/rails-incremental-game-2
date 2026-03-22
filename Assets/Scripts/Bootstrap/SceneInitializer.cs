using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private ResourceAmountsUI resourceAmountsUI;

    private void Awake()
    {
        resourceAmountsUI.Initialize();
    }
}
