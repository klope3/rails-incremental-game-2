using UnityEngine;

public class PlayerPickupCollector : PickupCollector
{
    [SerializeField] private ResourceHandler resourceHandler;
    [SerializeField] private AbilityHandler abilityHandler;
    [SerializeField] private ResourceRankHandler rankHandler;

    protected override void CollectPickup(PickupWorldInstance pickup)
    {
        PickupCollectContext context = new PickupCollectContext(resourceHandler, abilityHandler, rankHandler);
        pickup.GetCollected(context);
    }
}
