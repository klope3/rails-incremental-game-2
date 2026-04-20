using UnityEngine;

public class PlayerPickupCollector : PickupCollector
{
    [SerializeField] private ResourceHandler resourceHandler;
    [SerializeField] private AbilityHandler abilityHandler;

    protected override void CollectPickup(PickupWorldInstance pickup)
    {
        PickupCollectContext context = new PickupCollectContext(resourceHandler, abilityHandler);
        pickup.GetCollected(context);
    }
}
