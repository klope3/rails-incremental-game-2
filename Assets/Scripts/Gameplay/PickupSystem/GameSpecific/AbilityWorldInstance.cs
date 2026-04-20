using UnityEngine;

public class AbilityWorldInstance : PickupWorldInstance
{
    [SerializeField] private AbilitySO abilitySO;

    public override void GetCollected(PickupCollectContext context)
    {
        context.AbilityHandler.SetAbility(abilitySO);
    }
}
