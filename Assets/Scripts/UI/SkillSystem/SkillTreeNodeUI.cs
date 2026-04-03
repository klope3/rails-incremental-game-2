using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class SkillTreeNodeUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI debugNameText;
    [SerializeField] private TextMeshProUGUI debugStateText;
    [SerializeField] private UnityEngine.UI.Image image;
    [HideInInspector] public SkillTreeNodeSO nodeSO;
    public UnityEvent OnPreview;
    public UnityEvent OnPurchasable;
    public UnityEvent OnPurchased;
    public UnityEvent OnDistant;
    public event System.Action<SkillTreeNodeUI> OnClick;

    public void Initialize()
    {
        image.sprite = nodeSO.Sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(this);
    }

    public void UpdateVisuals(SkillHandler.SkillStatus status)
    {
        if (status == SkillHandler.SkillStatus.Distant)
        {
            OnDistant?.Invoke();
        }
        if (status == SkillHandler.SkillStatus.Preview)
        {
            OnPreview?.Invoke();
        }
        if (status == SkillHandler.SkillStatus.Purchasable)
        {
            OnPurchasable?.Invoke();
        }
        if (status == SkillHandler.SkillStatus.Purchased)
        {
            OnPurchased?.Invoke();
        }

        UpdateDebugText(status);
    }

    private void UpdateDebugText(SkillHandler.SkillStatus state)
    {
        debugNameText.text = nodeSO.SkillName;
        debugStateText.text = 
            state == SkillHandler.SkillStatus.Distant ? "HIDE" : 
            state == SkillHandler.SkillStatus.Preview ? "PREV" : 
            state == SkillHandler.SkillStatus.Purchasable ? "AVAIL" : "OWN";
    }
}
