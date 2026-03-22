using UnityEngine;

public class ResourceWorldInstance : MonoBehaviour
{
    [field: SerializeField] public ResourceSO ResourceSO { get; private set; }

    public void GetCollected()
    {
        gameObject.SetActive(false);
    }
}
