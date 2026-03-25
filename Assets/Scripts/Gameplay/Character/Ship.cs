using UnityEngine;

public class Ship : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        WorldObjectSpawnable obj = collision.collider.GetComponent<WorldObjectSpawnable>();
        if (obj == null)
        {
            return;
        }

        obj.ReceivePlayerImpact(this);
    }
}
