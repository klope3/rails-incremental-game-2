using System.Collections.Generic;
using UnityEngine;

//MonoBehaviour that lives in the scene so that it can reference various game-specific components and use their data to create a new SaveData object, 
//which is then passed to the project-agnostic SaveSystem.
public class SaveHelper : MonoBehaviour
{
    [SerializeField] private ResourceHandler resourceHandler;
    [SerializeField] private PlayerSkills playerSkills;

    [Sirenix.OdinInspector.Button]
    public void Save()
    {
        SaveData saveData = new SaveData();
        
        saveData.playerData.resource1Amount = resourceHandler.GetResourceAmount(0);
        saveData.playerData.resource2Amount = resourceHandler.GetResourceAmount(1);
        saveData.playerData.resource3Amount = resourceHandler.GetResourceAmount(2);

        List<OwnedSkill> ownedSkills = new List<OwnedSkill>();
        foreach (KeyValuePair<SkillTreeNodeSO, int> keyValuePair in playerSkills.OwnedSkills)
        {
            ownedSkills.Add(new OwnedSkill(keyValuePair.Key.Id, keyValuePair.Value));
        }

        saveData.playerData.ownedSkills = ownedSkills.ToArray();

        SaveSystem.Save(saveData);
    }
}
