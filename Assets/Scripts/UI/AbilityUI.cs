using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private AbilityHandler abilityHandler;
    [SerializeField] private Image image;
    [SerializeField] private Image usesBar;

    public void Initialize()
    {
        UpdateUI();
        abilityHandler.OnAbilityUpdate += AbilityHandler_OnAbilityUpdate;
    }

    private void AbilityHandler_OnAbilityUpdate()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        AbilitySO abilitySO = abilityHandler.EquippedAbility;
        image.sprite = abilitySO != null ? abilitySO.Sprite : null;
        image.gameObject.SetActive(abilitySO != null);
        usesBar.fillAmount = abilitySO != null ? (float)abilityHandler.EquippedAbilityUsesRemaining / abilitySO.BaseMaxUses : 0;
    }
}
