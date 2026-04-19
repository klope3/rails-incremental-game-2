using UnityEngine;

[CreateAssetMenu(fileName = "LaserAbility", menuName = "Scriptable Objects/AbilitySystem/LaserAbility")]
public class LaserAbility : AbilitySO
{
    public override void ActivateAbility(AbilityUseContext context)
    {
        context.PlayerLaser.Fire();
    }

    public override void DeactivateAbility(AbilityUseContext context)
    {

    }
}
