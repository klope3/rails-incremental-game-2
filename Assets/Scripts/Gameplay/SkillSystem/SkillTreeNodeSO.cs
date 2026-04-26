using UnityEngine;

//Represents an individual node of a skill tree. Each node can have multiple tiers, but needs to have at least one to do anything.
//Keep a list somewhere in game state to represent which nodes a character "owns," and the tier at which they own it.
[CreateAssetMenu(fileName = "SkillTreeNodeSO", menuName = "Scriptable Objects/SkillSystem/SkillTreeNodeSO")]
public class SkillTreeNodeSO : ScriptableObject
{
    [field: SerializeField] public string SkillName { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public Vector2Int GridPosition { get; private set; }
    [field: SerializeField] public SkillTreeNodeSO[] Children { get; private set; }
    [field: SerializeField] public SkillTierSO[] Tiers { get; private set; }

    public void ApplySkill(AppliedSkillEffects effects, int tierIndex)
    {
        SkillTierSO tier = GetTier(tierIndex);
        tier.ApplyTierEffect(effects);
    }

    public bool AreTierRequirementsMet(SkillRequirementContext context, int tierIndex)
    {
        SkillTierSO tier = GetTier(tierIndex);
        return tier.AreRequirementsMet(context);
    }

    private SkillTierSO GetTier(int index)
    {
        if (index < 0 || index >= Tiers.Length)
        {
            Debug.LogError($"Requested tier {index} is outside the range of the {Tiers.Length} tiers in {SkillName}");
            return null;
        }

        return Tiers[index];
    }

    public void ApplyRequirements(SkillRequirementContext context, int tierIndex)
    {
        SkillTierSO tier = GetTier(tierIndex);
        foreach (SkillTierRequirement req in tier.Requirements)
        {
            SkillRequirements.ApplyRequirement(req, context);
        }
    }
}
