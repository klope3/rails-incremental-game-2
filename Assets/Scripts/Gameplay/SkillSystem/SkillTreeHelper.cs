using UnityEngine;

//helper component that allows us to analyze the skill tree in some useful ways.
//e.g. answering the question "does the player currently have the parent of X node unlocked?" would be more difficult without this helper.
//this could probably be made generic and reusable but I don't have time for that right now.
public class SkillTreeHelper : MonoBehaviour
{
    [field: SerializeField] public SkillTreeNodeSO RootNode { get; private set; }

    public SkillTreeNodeSO Find(System.Func<SkillTreeNodeSO, bool> match)
    {
        return FindRecursive(match, RootNode);
    }

    public SkillTreeNodeSO GetParentOf(SkillTreeNodeSO targetChildNode)
    {
        if (targetChildNode == null || targetChildNode == RootNode) return null;

        return GetParentRecursive(targetChildNode, RootNode, null);
    }

    private SkillTreeNodeSO GetParentRecursive(SkillTreeNodeSO targetChildNode, SkillTreeNodeSO currentNode, SkillTreeNodeSO parentOfCurrentNode)
    {
        if (currentNode == targetChildNode) return parentOfCurrentNode;

        foreach (SkillTreeNodeSO child in currentNode.Children)
        {
            SkillTreeNodeSO result = GetParentRecursive(targetChildNode, child, currentNode);
            if (result != null) return result;
        }

        return null;
    }

    private SkillTreeNodeSO FindRecursive(System.Func<SkillTreeNodeSO, bool> match, SkillTreeNodeSO currentNode)
    {
        if (match(currentNode)) return currentNode;

        foreach (SkillTreeNodeSO child in currentNode.Children)
        {
            SkillTreeNodeSO result = FindRecursive(match, child);
            if (result != null) return result;
        }

        return null;
    }
}
