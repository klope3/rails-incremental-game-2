using UnityEngine;

[CreateAssetMenu(fileName = "DashAbility", menuName = "Scriptable Objects/AbilitySystem/DashAbility")]
public class DashAbility : AbilitySO
{
    [SerializeField] private bool powerful; //whether to do a "power dash"

    public override void ActivateAbility(AbilityUseContext context)
    {
        context.PlayerDash.DoDash(powerful);
    }

    public override void DeactivateAbility(AbilityUseContext context)
    {

    }
}
