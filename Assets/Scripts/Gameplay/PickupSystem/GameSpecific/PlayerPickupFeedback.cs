using UnityEngine;
using UnityEngine.Events;

public class PlayerPickupFeedback : MonoBehaviour
{
    [SerializeField] private PickupCollector playerPickupCollector;
    public UnityEvent OnResourcePickup;

    private void Awake()
    {
        playerPickupCollector.OnCollect += PlayerPickupCollector_OnCollect;
    }

    private void PlayerPickupCollector_OnCollect(GameObject obj)
    {
        ResourceWorldInstance resource = obj.GetComponent<ResourceWorldInstance>();
        if (resource != null)
        {
            OnResourcePickup?.Invoke();
        }
    }
}
