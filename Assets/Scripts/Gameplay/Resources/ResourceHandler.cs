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
    public void AddResource(ResourceSO resourceSO, int amount)
    {
        int existingAmount = GetResourceAmount(resourceSO);
        resourceAmounts[resourceSO] = Mathf.Clamp(existingAmount + amount, 0, int.MaxValue);
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
