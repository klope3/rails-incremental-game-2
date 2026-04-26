using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

//Handles storing the player's owned skills. Other components should reach into the AppliedSkillEffects context object to see what modifiers what might be present, and use them accordingly.
//For instance, a character controller might find extra move speed in there. It could then modify the character's move speed accordingly.
public class PlayerSkills : MonoBehaviour
{
    [ShowInInspector, ReadOnly] private Dictionary<SkillTreeNodeSO, int> ownedSkills; //each entry is a skill the player owns, alongside the tier at which they own it; tiers are zero-indexed
    public AppliedSkillEffects AppliedSkillEffects { get; private set; }
    public event System.Action OnSkillsChange;

    public void Initialize()
    {
        ownedSkills = new Dictionary<SkillTreeNodeSO, int>();
        AppliedSkillEffects = new AppliedSkillEffects();
    }

    [Button]
    public void AddSkill(SkillTreeNodeSO skillTreeNodeSO)
    {
        if (HasSkill(skillTreeNodeSO))
        {
            Debug.LogError($"Player already owns skill {skillTreeNodeSO.SkillName}");
            return;
        }
        ownedSkills.Add(skillTreeNodeSO, 0);
        UpdateSkillEffectsObject();
        OnSkillsChange?.Invoke();

        Debug.Log($"Added skill '{skillTreeNodeSO.SkillName}'");
    }

    public void UpgradeSkill(SkillTreeNodeSO skillTreeNodeSO)
    {
        if (!HasSkill(skillTreeNodeSO))
        {
            Debug.LogError($"Player doesn't own skill {skillTreeNodeSO.SkillName}");
            return;
        }

        int curTierIndex = ownedSkills[skillTreeNodeSO];
        if (curTierIndex + 1 >= skillTreeNodeSO.Tiers.Length)
        {
            Debug.LogError($"Player already has tier {curTierIndex} of {skillTreeNodeSO.SkillName} and the skill only has {skillTreeNodeSO.Tiers.Length} tiers");
            return;
        }

        ownedSkills[skillTreeNodeSO] = curTierIndex + 1;
        UpdateSkillEffectsObject();
        OnSkillsChange?.Invoke();

        Debug.Log($"Upgraded skill '{skillTreeNodeSO.SkillName}' to tier {ownedSkills[skillTreeNodeSO]}");
    }

    [Button]
    public void RemoveAllSkills()
    {
        ownedSkills = new Dictionary<SkillTreeNodeSO, int>();
        UpdateSkillEffectsObject();
        OnSkillsChange?.Invoke();
    }

    public void UpdateSkillEffectsObject()
    {
        AppliedSkillEffects = new AppliedSkillEffects();
        foreach (KeyValuePair<SkillTreeNodeSO, int> pair in ownedSkills)
        {
            SkillTreeNodeSO skill = pair.Key;
            int tier = pair.Value;
            skill.ApplySkill(AppliedSkillEffects, tier);
        }
    }

    public bool HasSkill(SkillTreeNodeSO skill)
    {
        if (skill == null) return false;

        return ownedSkills.ContainsKey(skill);
    }

    public bool HasSkill(SkillTreeNodeSO skill, out int ownedTierIndex)
    {
        if (skill == null)
        {
            ownedTierIndex = -1;
            return false;
        }

        bool owned = ownedSkills.TryGetValue(skill, out ownedTierIndex);
        return owned;
    }
}
