using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents an individual node of a skill tree. Keep a list somewhere in game state to represent which nodes a character "owns."
//Each node must have one or more "effects" to do anything.
[CreateAssetMenu(fileName = "SkillTreeNodeSO", menuName = "Scriptable Objects/SkillSystem/SkillTreeNodeSO")]
public class SkillTreeNodeSO : ScriptableObject
{
    [field: SerializeField] public string SkillName { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public Vector2Int GridPosition { get; private set; }
    [field: SerializeField] public SkillTreeNodeSO[] Children { get; private set; }
    [SerializeField] private SkillEffectSO[] effects;
    [SerializeField] private SkillRequirementSO[] requirements;

    public void ApplySkill(AppliedSkillEffects effects)
    {
        foreach (SkillEffectSO effect in this.effects)
        {
            effect.ApplyEffect(effects);
        }
    }

    public bool AreRequirementsMet(SkillRequirementContext context)
    {
        foreach (SkillRequirementSO req in requirements)
        {
            if (!req.IsRequirementMet(context)) return false;
        }

        return true;
    }

    public void ApplyRequirements(SkillRequirementContext context)
    {
        foreach (SkillRequirementSO req in requirements)
        {
            req.ApplyRequirement(context);
        }
    }
}
