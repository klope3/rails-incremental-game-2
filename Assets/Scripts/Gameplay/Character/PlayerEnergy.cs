using UnityEngine;
using UnityEngine.Events;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] private float baseAmount;
    [SerializeField] private float baseDepletionRate;
    public float CurrentAmount { get; private set; }
    public float MaxAmount
    {
        get
        {
            return baseAmount; //this may eventually be modified via upgrades
        }
    }
    public UnityEvent OnDepleted;

    private void Awake()
    {
        CurrentAmount = baseAmount;
    }

    private void Update()
    {
        Add(-1 * baseDepletionRate * Time.deltaTime);
    }

    public void Add(float amount)
    {
        CurrentAmount = Mathf.Clamp(CurrentAmount + amount, 0, MaxAmount);
        if (Mathf.Approximately(CurrentAmount, 0))
        {
            OnDepleted?.Invoke();
        }
    }
}
