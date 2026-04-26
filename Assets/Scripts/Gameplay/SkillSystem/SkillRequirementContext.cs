//context object passed through each of a skill tier's requirement objects. used for checking if requirements are met for purchasing a skill, as well as applying those requirements (e.g. deducting money).
//This class needs to exist for the skill system to work, but the data structure inside it is project-specific.
//for example, some projects might require certain levels to be complete for certain skill tiers to be purchased, while others are only money-based.
public class SkillRequirementContext
{
    public ResourceHandler ResourceHandler { get; private set; }

    public SkillRequirementContext(ResourceHandler resourceHandler)
    {
        ResourceHandler = resourceHandler;
    }
}
