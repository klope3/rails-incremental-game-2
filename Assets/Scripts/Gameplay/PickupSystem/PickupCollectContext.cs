//Context object that gets passed to the PickupWorldInstance, which can then apply "get collected" logic to in its own unique ways.
//This class needs to exist for the pickup system to work, but the data structure inside it is project-specific.
public class PickupCollectContext
{
    public ResourceHandler ResourceHandler { get; private set; }
    public AbilityHandler AbilityHandler { get; private set; }

    public PickupCollectContext(ResourceHandler resourceHandler, AbilityHandler abilityHandler)
    {
        ResourceHandler = resourceHandler;
        AbilityHandler = abilityHandler;
    }
}
