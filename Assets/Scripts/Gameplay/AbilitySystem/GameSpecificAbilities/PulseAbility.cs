using UnityEngine;

[CreateAssetMenu(fileName = "PulseAbility", menuName = "Scriptable Objects/AbilitySystem/PulseAbility")]
public class PulseAbility : AbilitySO
{
    public override void ActivateAbility(AbilityUseContext context)
    {
        context.PlayerPulse.DoPulse();
    }

    public override void DeactivateAbility(AbilityUseContext context)
    {

    }
}
