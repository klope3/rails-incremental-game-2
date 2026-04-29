using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class ResourceHandler : MonoBehaviour
{
    //private static ResourceHandler _instance;
    //public static ResourceHandler Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = FindAnyObjectByType<ResourceHandler>();
    //        }
    //        return _instance;
    //    }
    //}

    [SerializeField] private ResourceRankHandler rankHandler;
    [SerializeField] private ResourceSO[] _resourceTypes;
    public IReadOnlyCollection<ResourceSO> ResourceTypes => _resourceTypes;
    [ShowInInspector, ReadOnly] private Dictionary<ResourceSO, int> resourceAmounts;
    public UnityEvent OnResourcesChanged;

    public void Initialize()
    {
        resourceAmounts = new Dictionary<ResourceSO, int>();
        foreach (ResourceSO resource in _resourceTypes)
        {
            resourceAmounts.Add(resource, 0);
        }

        OnResourcesChanged?.Invoke();
    }

    public void ApplySaveData(PlayerData playerData)
    {
        resourceAmounts[_resourceTypes[0]] = playerData.resource1Amount;
        resourceAmounts[_resourceTypes[1]] = playerData.resource2Amount;
        resourceAmounts[_resourceTypes[2]] = playerData.resource3Amount;

        OnResourcesChanged?.Invoke();
    }

    [Button]
    public void AddResource(ResourceSO resourceSO, int amount, bool bypassRank = false)
    {
        int existingAmount = GetResourceAmount(resourceSO);
        int amountToAdd = amount;
        if (!bypassRank && amountToAdd > 0)
        {
            int rank = rankHandler.GetResourceRank(resourceSO);
            amountToAdd *= rank;
        }
        resourceAmounts[resourceSO] = Mathf.Clamp(existingAmount + amountToAdd, 0, int.MaxValue);
        OnResourcesChanged?.Invoke();
    }

    //convenience overload to modify a resource amount without needing a reference to the SO
    public void AddResource(SkillRequirements.SkillRequirementType requirementType, int amount, bool bypassRank = false)
    {
        if (requirementType == SkillRequirements.SkillRequirementType.Resource1) AddResource(_resourceTypes[0], amount, bypassRank);
        if (requirementType == SkillRequirements.SkillRequirementType.Resource2) AddResource(_resourceTypes[1], amount, bypassRank);
        if (requirementType == SkillRequirements.SkillRequirementType.Resource3) AddResource(_resourceTypes[2], amount, bypassRank);
    }

    public int GetResourceAmount(ResourceSO resourceSO)
    {
        //if (resourceAmounts == null) InitializeDictionary();

        bool existing = resourceAmounts.TryGetValue(resourceSO, out int existingAmount);
        if (!existing)
        {
            Debug.LogError($"Resource {resourceSO.Name} is missing from the dictionary");
        }

        return existingAmount;
    }

    //convenience overload to get a required resource amount without needing a reference to the SO
    //this should replace the other overload
    public int GetResourceAmount(int index)
    {
        if (index < 0 || index >= _resourceTypes.Length)
        {
            Debug.LogError($"Index {index} is out of bounds");
            return -1;
        }

        return GetResourceAmount(_resourceTypes[index]);
    }

    //convenience overload to get a required resource amount without needing a reference to the SO
    public int GetResourceAmount(SkillRequirements.SkillRequirementType requirementType)
    {
        if (requirementType == SkillRequirements.SkillRequirementType.Resource1) return GetResourceAmount(_resourceTypes[0]);
        if (requirementType == SkillRequirements.SkillRequirementType.Resource2) return GetResourceAmount(_resourceTypes[1]);
        if (requirementType == SkillRequirements.SkillRequirementType.Resource3) return GetResourceAmount(_resourceTypes[2]);

        Debug.LogError($"Requirement type {requirementType} does not correspond to a resource type");
        return -1;
    }
}
