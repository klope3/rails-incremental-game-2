using UnityEngine;
using UnityEngine.Events;

public class ResourceWorldInstanceOld : MonoBehaviour
{
    [field: SerializeField] public ResourceSO ResourceSO { get; private set; }
    [SerializeField] private float deactivateDelay;
    public UnityEvent OnCollect;
    public UnityEvent OnEnabled;

    private void OnEnable()
    {
        OnEnabled?.Invoke();
    }

    public void GetCollected()
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
