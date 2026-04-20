using UnityEngine;
using Sirenix.OdinInspector;

public class AbilityHandler : MonoBehaviour
{
    [SerializeField] private PlayerDash playerDash;
    [SerializeField] private PlayerPulse playerPulse;
    [SerializeField] private PlayerLaser playerLaser;
    [ShowInInspector, ReadOnly] public AbilitySO EquippedAbility { get; private set; }
    [ShowInInspector, DisplayAsString] public int EquippedAbilityUsesRemaining { get; private set; }
    public event System.Action OnAbilityUpdate; //using, losing, or acquiring

    private void OnEnable()
    {
        InputActionsProvider.OnClickStarted += InputActionsProvider_OnClickStarted;
        InputActionsProvider.OnClickCanceled += InputActionsProvider_OnClickCanceled;
    }
    private void OnDisable()
    {
        InputActionsProvider.OnClickStarted -= InputActionsProvider_OnClickStarted;
        InputActionsProvider.OnClickCanceled -= InputActionsProvider_OnClickCanceled;
    }

    private void InputActionsProvider_OnClickStarted()
    {
        StartEquippedAbility();
    }

    private void InputActionsProvider_OnClickCanceled()
    {
        StopEquippedAbility();
    }

    public void SetAbility(AbilitySO abilitySO)
    {
        EquippedAbility = abilitySO;
        EquippedAbilityUsesRemaining = abilitySO.BaseMaxUses;
        OnAbilityUpdate?.Invoke();
    }

    public void StartEquippedAbility()
    {
        if (EquippedAbility == null)
        {
            return;
        }
        AbilityUseContext context = new AbilityUseContext(playerDash, playerPulse, playerLaser);
        EquippedAbility.ActivateAbility(context);
        EquippedAbilityUsesRemaining--;
        if (EquippedAbilityUsesRemaining == 0)
        {
            EquippedAbility = null;
        }
        OnAbilityUpdate?.Invoke();
    }

    //only used for abilities where something happens on mouse up (such as the ability turning off)
    public void StopEquippedAbility()
    {
        if (EquippedAbility == null)
        {
            return;
        }
        AbilityUseContext context = new AbilityUseContext(playerDash, playerPulse, playerLaser);
        EquippedAbility.DeactivateAbility(context);
    }
}
