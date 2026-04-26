using UnityEngine;

//handles transactions, evaluation of specific skills, etc.
public class SkillHandler : MonoBehaviour
{
    [SerializeField] private ResourceHandler resourceHandler;
    [SerializeField] private PlayerSkills playerSkills;
    [SerializeField] private SkillTreeHelper skillTreeHelper;
    public enum SkillStatus
    {
        Null, //a default value that should never occur
        Distant, //anything farther away than preview; often this is hidden completely
        Preview, //it's adjacent to a purchasable node, so it's "just out of reach"
        Purchasable, //its parent has been purchased, so it's eligible for puchase too
        Purchased //already purchased
    }

    public SkillStatus GetSkillStatus(SkillTreeNodeSO node)
    {
        if (node == null) return SkillStatus.Null;

        bool owned = playerSkills.HasSkill(node);
        if (!owned && node == skillTreeHelper.RootNode)
        {
            return SkillStatus.Purchasable; //only valid states for the root node are "purchasable" (at very start of game) or "owned"
        }

        SkillTreeNodeSO parentOfNode = skillTreeHelper.GetParentOf(node);
        bool parentOwned = playerSkills.HasSkill(parentOfNode);

        if (owned)
        {
            return SkillStatus.Purchased;
        }
        if (!owned && parentOwned)
        {
            return SkillStatus.Purchasable;
        }
        if (!owned && !parentOwned)
        {
            return SkillStatus.Preview;
        }

        return SkillStatus.Distant;
    }

    public void TryPurchase(SkillTreeNodeSO skill)
    {
        SkillStatus status = GetSkillStatus(skill);
        if (status != SkillStatus.Purchasable && status != SkillStatus.Purchased)
        {
            Debug.Log($"Purchase not allowed; the skill's status is '{status}'");
            return;
        }

        bool hasSkill = playerSkills.HasSkill(skill, out int ownedTierIndex);
        int tierIndexToPurchase = hasSkill ? ownedTierIndex + 1 : 0;
        bool alreadyHighestTier = tierIndexToPurchase >= skill.Tiers.Length;
        if (alreadyHighestTier)
        {
            Debug.Log("Purchase not allowed; already highest tier");
            return;
        }

        SkillRequirementContext context = new SkillRequirementContext(resourceHandler);
        bool requirementsMet = skill.AreTierRequirementsMet(context, tierIndexToPurchase);
        if (!requirementsMet)
        {
            Debug.Log($"Purchase not allowed; at least one requirement of tier {tierIndexToPurchase} not met");
            return;
        }

        skill.ApplyRequirements(context, tierIndexToPurchase);

        if (hasSkill)
        {
            playerSkills.UpgradeSkill(skill);
        } else
        {
            playerSkills.AddSkill(skill);
        }
    }
}
