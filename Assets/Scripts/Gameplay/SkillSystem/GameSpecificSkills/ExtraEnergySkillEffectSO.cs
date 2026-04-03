using UnityEngine;

[CreateAssetMenu(fileName = "ExtraEnergySkillEffectSO", menuName = "Scriptable Objects/SkillSystem/ExtraEnergySkillEffectSO")]
public class ExtraEnergySkillEffectSO : SkillEffectSO
{
    [field: SerializeField] public float Amount { get; private set; }

    public override void ApplyEffect(AppliedSkillEffects effects)
    {
        effects.EnergyAdd += Amount;
    }
}
