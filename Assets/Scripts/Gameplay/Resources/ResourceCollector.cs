using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ResourceCollector : MonoBehaviour
{
    [SerializeField] private ResourceHandler resourceHandler;
    public UnityEvent OnCollect;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        ResourceWorldInstanceOld resourceInstance = other.GetComponent<ResourceWorldInstanceOld>();
        if (resourceInstance == null)
        {
            return;
        }

        resourceHandler.AddResource(resourceInstance.ResourceSO, 1);
        resourceInstance.GetCollected();
        OnCollect?.Invoke();
    }
}
