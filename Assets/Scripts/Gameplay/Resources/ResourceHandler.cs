using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class ResourceHandler : MonoBehaviour
{
    private static ResourceHandler _instance;
    public static ResourceHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<ResourceHandler>();
            }
            return _instance;
        }
    }

    [SerializeField] private ResourceRankHandler rankHandler;
    [SerializeField] private ResourceSO[] _resourceTypes;
    public IReadOnlyCollection<ResourceSO> ResourceTypes => _resourceTypes;
    [ShowInInspector, ReadOnly] private Dictionary<ResourceSO, int> resourceAmounts;
    public UnityEvent OnResourcesChanged;

    private void InitializeDictionary()
    {
        resourceAmounts = new Dictionary<ResourceSO, int>();
        foreach (ResourceSO resource in _resourceTypes)
        {
            resourceAmounts.Add(resource, 0);
        }

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

    public int GetResourceAmount(ResourceSO resourceSO)
    {
        if (resourceAmounts == null) InitializeDictionary();

        bool existing = resourceAmounts.TryGetValue(resourceSO, out int existingAmount);
        if (!existing)
        {
            Debug.LogError($"Resource {resourceSO.Name} is missing from the dictionary");
        }

        return existingAmount;
    }
}
