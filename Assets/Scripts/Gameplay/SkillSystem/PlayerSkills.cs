using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

//Handles storing the player's owned skills. Other components should reach into the AppliedSkillEffects context object to see what modifiers what might be present, and use them accordingly.
//For instance, a character controller might find extra move speed in there. It could then modify the character's move speed accordingly.
public class PlayerSkills : MonoBehaviour
{
    private HashSet<SkillTreeNodeSO> ownedSkills;
    public AppliedSkillEffects AppliedSkillEffects { get; private set; }
    public event System.Action OnSkillsChange;

    public void Initialize()
    {
        ownedSkills = new HashSet<SkillTreeNodeSO>();
        AppliedSkillEffects = new AppliedSkillEffects();
    }

    [Button]
    public void AddSkill(SkillTreeNodeSO skillTreeNodeSO)
    {
        ownedSkills.Add(skillTreeNodeSO);
        UpdateSkillEffectsObject();
        OnSkillsChange?.Invoke();
    }

    [Button]
    public void RemoveAllSkills()
    {
        ownedSkills = new HashSet<SkillTreeNodeSO>();
        UpdateSkillEffectsObject();
        OnSkillsChange?.Invoke();
    }

    public void UpdateSkillEffectsObject()
    {
        AppliedSkillEffects = new AppliedSkillEffects();
        foreach (SkillTreeNodeSO skill in ownedSkills)
        {
            skill.ApplySkill(AppliedSkillEffects);
        }
    }

    public bool HasSkill(SkillTreeNodeSO skill)
    {
        if (skill == null) return false;

        SkillTreeNodeSO match = ownedSkills.FirstOrDefault(item => item == skill);
        return match != null;
    }
}
