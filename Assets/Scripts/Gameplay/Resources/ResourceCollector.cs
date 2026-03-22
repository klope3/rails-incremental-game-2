using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ResourceCollector : MonoBehaviour
{
    [SerializeField] private ResourceHandler resourceHandler;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        ResourceWorldInstance resourceInstance = other.GetComponent<ResourceWorldInstance>();
        if (resourceInstance == null)
        {
            return;
        }

        resourceHandler.AddResource(resourceInstance.ResourceSO, 1);
        resourceInstance.GetCollected();
    }
}
