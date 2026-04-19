using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private ResourceAmountsUI resourceAmountsUI;
    [SerializeField] private PlayerSkills playerSkills;
    [SerializeField] private GameFlow gameFlow;
    [SerializeField] private SkillTreeUI skillTreeUI;
    [SerializeField] private GameDebug gameDebug;

    private void Awake()
    {
        InputActionsProvider.Initialize();

        playerSkills.Initialize();
        gameFlow.Initialize();

        skillTreeUI.Initialize();
        resourceAmountsUI.Initialize();

        gameDebug.Initialize();
    }
}
