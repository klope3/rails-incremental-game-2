using UnityEngine;

public class SceneInitializerGame : MonoBehaviour
{
    [SerializeField] private ResourceHandler resourceHandler;
    [SerializeField] private ResourceAmountsUI resourceAmountsUI;
    [SerializeField] private PlayerSkills playerSkills;
    [SerializeField] private PlayerEnergy playerEnergy;
    [SerializeField] private GameFlow gameFlow;
    [SerializeField] private SkillTreeUI skillTreeUI;
    [SerializeField] private AbilityUI abilityUI;
    [SerializeField] private GameDebug gameDebug;

    private void Awake()
    {
        InputActionsProvider.Initialize();

        playerSkills.Initialize();
        playerEnergy.Initialize();
        gameFlow.Initialize();

        skillTreeUI.Initialize();
        resourceHandler.Initialize();
        resourceAmountsUI.Initialize();
        abilityUI.Initialize();

        ApplyLoadedSaveData();

        gameDebug.Initialize();
    }

    private void ApplyLoadedSaveData()
    {
        LoadedSaveData loadedSaveData = FindAnyObjectByType<LoadedSaveData>();
        if (loadedSaveData == null) return;

        playerSkills.ApplySaveData(loadedSaveData.SaveData.playerData);
        resourceHandler.ApplySaveData(loadedSaveData.SaveData.playerData);
    }
}
