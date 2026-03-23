using UnityEngine;

public class WorldObjectSpawnable : MonoBehaviour
{
    [SerializeField, Tooltip("The object will be deactivated when it passes this Z-coordinate.")] 
    private float minZ;

    private void Update()
    {
        if (transform.position.z < minZ)
        {
            gameObject.SetActive(false);
        }
    }
}
