using System.Collections.Generic;
using UnityEngine;

public class ResourceAmountsUI : MonoBehaviour
{
    [SerializeField] private ResourceHandler resourceHandler;
    [SerializeField] private ResourceRankHandler rankHandler;
    [SerializeField] private ResourceAmountElementUI resourceAmountElementPf;
    [SerializeField] private RectTransform resouceAmountElementsParent;
    private List<ResourceAmountElementUI> resourceAmountElements;

    public void Initialize()
    {
        resourceAmountElements = new List<ResourceAmountElementUI>();
        foreach (ResourceSO resourceType in resourceHandler.ResourceTypes)
        {
            ResourceAmountElementUI newElement = Instantiate(resourceAmountElementPf, resouceAmountElementsParent);
            newElement.Initialize(resourceHandler, rankHandler, resourceType);
            resourceAmountElements.Add(newElement);
        }
    }
    
    public void UpdateDisplay()
    {
        foreach (ResourceAmountElementUI resourceAmountElement in resourceAmountElements)
        {
            resourceAmountElement.UpdateDisplay();
        }
    }
}
