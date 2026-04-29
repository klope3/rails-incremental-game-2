using System.Collections.Generic;
using UnityEngine;

public static class GameDatabase
{
    private static ResourceDictionary<SkillTreeNodeSO> _skillNodes;
    private static ResourceDictionary<SkillTreeNodeSO> SkillNodes
    {
        get
        {
            if (_skillNodes == null)
            {
                InitializeSkillNodes();
            }
            return _skillNodes;
        }
    }

    private static void InitializeSkillNodes()
    {
        _skillNodes = new ResourceDictionary<SkillTreeNodeSO>("SkillTreeNodeSO");
    }

    public static SkillTreeNodeSO GetSkillNode(string id)
    {
        bool exists = SkillNodes.TryGetItem(id, out SkillTreeNodeSO skill);
        if (!exists)
        {
            Debug.LogError($"Skill with ID '{id}' not found in database");
            return null;
        }

        return skill;
    }

    public static void Reinitialize()
    {
        InitializeSkillNodes();
    }
}
