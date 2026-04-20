using UnityEngine;
using UnityEngine.Events;

public abstract class PickupWorldInstance : MonoBehaviour
{
    [SerializeField] private float deactivateDelay;
    public UnityEvent OnCollect;
    public UnityEvent OnEnabled;

    private void OnEnable()
    {
        OnEnabled?.Invoke();
    }

    public abstract void GetCollected(PickupCollectContext context);

    public void AfterCollect()
    {
        OnCollect?.Invoke();
        StartCoroutine(CO_Deactivate());
    }

    private System.Collections.IEnumerator CO_Deactivate()
    {
        yield return new WaitForSeconds(deactivateDelay);
        gameObject.SetActive(false);
    }
}
