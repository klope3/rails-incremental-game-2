using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class PickupCollector : MonoBehaviour
{
    public event System.Action<GameObject> OnCollect;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PickupWorldInstance pickup = other.GetComponent<PickupWorldInstance>();
        if (pickup == null)
        {
            return;
        }

        CollectPickup(pickup);
        pickup.AfterCollect();
        OnCollect?.Invoke(other.gameObject);
    }

    protected abstract void CollectPickup(PickupWorldInstance pickup);
}
