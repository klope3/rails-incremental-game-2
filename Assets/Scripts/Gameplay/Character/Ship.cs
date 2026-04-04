using UnityEngine;

public class Ship : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        WorldObjectSpawnableNew obj = collision.collider.GetComponent<WorldObjectSpawnableNew>();
        if (obj == null)
        {
            return;
        }

        obj.ReceivePlayerImpact(this);
    }
}
