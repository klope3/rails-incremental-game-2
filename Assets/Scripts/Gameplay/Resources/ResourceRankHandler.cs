using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceRankHandler : MonoBehaviour
{
    [SerializeField] private ResourceHandler resourceHandler;
    [SerializeField] private int collectionsPerRank; //this will eventually increase per rank (e.g. rank 3 is harder to reach than rank 2)
    private Dictionary<ResourceSO, int> ranks;
    public int CurCollectionStreak { get; private set; } //collecting resource X three times in a row builds a streak of 3, but collecting resource Y resets the streak back to 1
    public int CollectionsToNextRank
    {
        get
        {
            return collectionsPerRank;
        }
    }
    private readonly int DEFAULT_RANK = 1;
    public ResourceSO LastCollectedResource { get; private set; }
    public event System.Action OnRankUpdate;
    public UnityEvent OnRankUpdated;

    public void PrepareForLevel()
    {
        ranks = new Dictionary<ResourceSO, int>();
        foreach (ResourceSO resourceSO in resourceHandler.ResourceTypes)
        {
            ranks.Add(resourceSO, DEFAULT_RANK);
        }
    }

    public int GetResourceRank(ResourceSO resourceSO)
    {
        if (ranks == null) return DEFAULT_RANK;

        bool exists = ranks.TryGetValue(resourceSO, out int value);
        if (!exists) return DEFAULT_RANK;

        return value;
    }

    public void RegisterResourceCollected(ResourceSO resourceSO)
    {
        if (LastCollectedResource == resourceSO)
        {
            CurCollectionStreak++;
        } else
        {
            CurCollectionStreak = 1;
        }
        LastCollectedResource = resourceSO;
        if (CurCollectionStreak == CollectionsToNextRank)
        {
            ranks[resourceSO]++;
            CurCollectionStreak = 0;
        }
        OnRankUpdate?.Invoke();
        OnRankUpdated?.Invoke();
    }
}
