using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceAmountElementUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Image image;
    private ResourceHandler resourceHandler;
    private ResourceRankHandler rankHandler;
    private ResourceSO resourceSO;

    public void Initialize(ResourceHandler resourceHandler, ResourceRankHandler rankHandler, ResourceSO resourceSO)
    {
        this.resourceHandler = resourceHandler;
        this.rankHandler = rankHandler;
        this.resourceSO = resourceSO;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        image.sprite = resourceSO.Sprite;
        image.color = resourceSO.Color;
        int amount = resourceHandler.GetResourceAmount(resourceSO);
        amountText.text = $"{amount}";
        int rank = rankHandler.GetResourceRank(resourceSO);
        rankText.text = rank > 1 ? $"x{rankHandler.GetResourceRank(resourceSO)}" : "";
    }
}
