using UnityEngine;

//represents a single "tier" or "level" of a specific skill node (e.g. "Extra Health Lvl. 4").
//Typically all tiers will have the same effect, but be more beneficial than the previous tiers (e.g. a greater increase to max health, or a greater decrease to the rate of the clock ticking down).
[CreateAssetMenu(fileName = "SkillTierSO", menuName = "Scriptable Objects/SkillSystem/SkillTierSO")]
public class SkillTierSO : ScriptableObject
{
    [field: SerializeField] public SkillEffects.SkillEffectType Effect { get; private set; }
    //params allow flexible control of how an effect type is applied, at the cost of clarity. they are expressed as floats but can represent ints, percentages, bools, etc. depending on the effect's implementation.
    //some effect types may leave the params completely unused.
    [field: SerializeField] public float Param1 { get; private set; }
    [field: SerializeField] public SkillTierRequirement[] Requirements { get; private set; }

    public bool AreRequirementsMet(SkillRequirementContext context)
    {
        foreach (SkillTierRequirement req in Requirements)
        {
            if (!SkillRequirements.IsRequirementMet(req, context))
            {
                return false;
            }
        }

        return true;
    }

    public void ApplyTierEffect(AppliedSkillEffects appliedSkillEffects)
    {
        if (Effect == SkillEffects.SkillEffectType.MaxEnergyIncrease)
        {
            SkillEffects.MaxEnergyIncrease(appliedSkillEffects, Param1);
            return;
        }
        if (Effect == SkillEffects.SkillEffectType.FasterEnergyRegen)
        {
            SkillEffects.FasterEnergyRegen(appliedSkillEffects, Param1);
            return;
        }
        if (Effect == SkillEffects.SkillEffectType.MoveSpeedIncrease)
        {
            SkillEffects.MoveSpeedIncrease(appliedSkillEffects, Param1);
            return;
        }

        Debug.LogError($"Skill effect {Effect} has no implementation");
    }
}
