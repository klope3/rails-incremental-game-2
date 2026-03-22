using UnityEngine;
using TMPro;

public class ResourceAmountElementUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountText;
    private ResourceHandler resourceHandler;
    private ResourceSO resourceSO;

    public void Initialize(ResourceHandler resourceHandler, ResourceSO resourceSO)
    {
        this.resourceHandler = resourceHandler;
        this.resourceSO = resourceSO;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        int amount = resourceHandler.GetResourceAmount(resourceSO);
        amountText.text = $"{amount} {resourceSO.Name}";
    }
}
