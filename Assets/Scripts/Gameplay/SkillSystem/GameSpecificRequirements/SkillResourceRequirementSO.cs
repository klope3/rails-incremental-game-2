using UnityEngine;

[CreateAssetMenu(fileName = "SkillResourceRequirementSO", menuName = "Scriptable Objects/SkillSystem/SkillResourceRequirementSO")]
public class SkillResourceRequirementSO : SkillRequirementSO
{
    [SerializeField] private int resource1Requirement;
    [SerializeField] private int resource2Requirement;
    [SerializeField] private int resource3Requirement;
    [SerializeField] private ResourceSO resource1SO;
    [SerializeField] private ResourceSO resource2SO;
    [SerializeField] private ResourceSO resource3SO;

    public override bool IsRequirementMet(SkillRequirementContext context)
    {
        int resource1Amount = context.ResourceHandler.GetResourceAmount(resource1SO);
        int resource2Amount = context.ResourceHandler.GetResourceAmount(resource2SO);
        int resource3Amount = context.ResourceHandler.GetResourceAmount(resource3SO);

        return resource1Amount >= resource1Requirement && resource2Amount >= resource2Requirement && resource3Amount >= resource3Requirement;
    }

    public override void ApplyRequirement(SkillRequirementContext context)
    {
        context.ResourceHandler.AddResource(resource1SO, -1 * resource1Requirement);
        context.ResourceHandler.AddResource(resource2SO, -1 * resource2Requirement);
        context.ResourceHandler.AddResource(resource3SO, -1 * resource3Requirement);
    }
}
