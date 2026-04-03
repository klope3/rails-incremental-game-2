using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private ResourceAmountsUI resourceAmountsUI;
    [SerializeField] private PlayerSkills playerSkills;
    [SerializeField] private GameFlow gameFlow;
    [SerializeField] private SkillTreeUI skillTreeUI;

    private void Awake()
    {
        resourceAmountsUI.Initialize();
        playerSkills.Initialize();
        gameFlow.Initialize();
        skillTreeUI.Initialize();
    }
}
