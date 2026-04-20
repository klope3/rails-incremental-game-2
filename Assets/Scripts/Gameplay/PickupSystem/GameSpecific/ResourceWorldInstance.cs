using UnityEngine;

public class ResourceWorldInstance : PickupWorldInstance
{
    [SerializeField] private ResourceSO resourceSO;

    public override void GetCollected(PickupCollectContext context)
    {
        context.ResourceHandler.AddResource(resourceSO, 1);
    }
}
