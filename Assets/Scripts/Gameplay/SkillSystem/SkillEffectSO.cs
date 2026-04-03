using UnityEngine;

//Represents a specific effect that can be added to a skill node. Override ApplyEffect to define unique effect behavior.
//Mutates an AppliedSkillEffects context object, which represents all skill effects currently applied to a character.
public class SkillEffectSO : ScriptableObject
{
    public virtual void ApplyEffect(AppliedSkillEffects effects) { }
}
