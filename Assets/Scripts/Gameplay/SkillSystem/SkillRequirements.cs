//represents all the requirement types that skill tiers in the game can have. this class must exist for the skill system to work, but its implementation will be game-specific.
public static class SkillRequirements
{
    public enum SkillRequirementType
    {
        Resource1,
        Resource2,
        Resource3,
    }

    public static bool IsRequirementMet(SkillTierRequirement requirement, SkillRequirementContext context)
    {
        SkillRequirementType requirementType = requirement.RequirementType;
        if (requirementType == SkillRequirementType.Resource1 || requirementType == SkillRequirementType.Resource2 || requirementType == SkillRequirementType.Resource3)
        {
            int amount = context.ResourceHandler.GetResourceAmount(requirementType);
            if (amount >= (int)requirement.Param1)
            {
                return true;
            }
        }

        return false;
    }

    public static void ApplyRequirement(SkillTierRequirement requirement, SkillRequirementContext context)
    {
        SkillRequirementType requirementType = requirement.RequirementType;
        if (requirementType == SkillRequirementType.Resource1 || requirementType == SkillRequirementType.Resource2 || requirementType == SkillRequirementType.Resource3)
        {
            context.ResourceHandler.AddResource(requirementType, -1 * (int)requirement.Param1, true);
        }
    }
}
