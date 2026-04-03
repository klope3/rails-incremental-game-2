using UnityEngine;

//represents a specific requirement to purchase/earn a skill. for example, 30 gold, or 2 carbon and 1 iron, or "has completed level 2".
public abstract class SkillRequirementSO : ScriptableObject
{
    public abstract bool IsRequirementMet(SkillRequirementContext context);

    //deduct money, resources, xp, etc.
    //should only be called if we've first confirmed that requirements are met
    public abstract void ApplyRequirement(SkillRequirementContext context); 
}
