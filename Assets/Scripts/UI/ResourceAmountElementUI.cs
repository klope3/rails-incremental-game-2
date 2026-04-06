using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceAmountElementUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Image image;
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
        image.sprite = resourceSO.Sprite;
        image.color = resourceSO.Color;
        int amount = resourceHandler.GetResourceAmount(resourceSO);
        amountText.text = $"{amount}";
    }
}
