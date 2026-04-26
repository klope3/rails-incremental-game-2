using UnityEngine;
using UnityEngine.Events;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] private float baseAmount;
    [SerializeField] private float baseDepletionRate;
    [SerializeField] private PlayerSkills playerSkills;
    public bool EnergyCanDrain { get; set; }
    public float CurrentAmount { get; private set; }
    public float MaxAmount
    {
        get
        {
            return baseAmount + playerSkills.AppliedSkillEffects.EnergyAdd;
        }
    }
    public event System.Action OnDepleted;

    public void Initialize()
    {
        EnergyCanDrain = true;
    }

    private void Update()
    {
        Add(-1 * (baseDepletionRate + playerSkills.AppliedSkillEffects.EnergyDepletionRateAdd) * Time.deltaTime);
    }

    public void PrepareForLevel()
    {
        CurrentAmount = MaxAmount;
    }

    public void Add(float amount)
    {
        if (amount < 0 && !EnergyCanDrain)
        {
            return;
        }

        CurrentAmount = Mathf.Clamp(CurrentAmount + amount, 0, MaxAmount);
        if (Mathf.Approximately(CurrentAmount, 0))
        {
            OnDepleted?.Invoke();
        }
    }
}
