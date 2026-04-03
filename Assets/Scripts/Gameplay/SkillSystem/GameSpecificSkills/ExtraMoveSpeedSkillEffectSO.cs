using UnityEngine;

[CreateAssetMenu(fileName = "ExtraMoveSpeedSkillEffectSO", menuName = "Scriptable Objects/SkillSystem/ExtraMoveSpeedSkillEffectSO")]
public class ExtraMoveSpeedSkillEffectSO : SkillEffectSO
{
    [field: SerializeField] public float Amount { get; private set; }

    public override void ApplyEffect(AppliedSkillEffects effects)
    {
        effects.MoveSpeedAdd += Amount;
    }
}
