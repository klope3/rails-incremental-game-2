using UnityEngine;

[CreateAssetMenu(fileName = "ReducedEnergyDepletionSkillEffectSO", menuName = "Scriptable Objects/SkillSystem/ReducedEnergyDepletionSkillEffectSO")]
public class ReducedEnergyDepletionSkillEffectSO : SkillEffectSO
{
    [field: SerializeField] public float Amount { get; private set; }

    public override void ApplyEffect(AppliedSkillEffects effects)
    {
        effects.EnergyDepletionRateAdd += Amount;
    }
}
