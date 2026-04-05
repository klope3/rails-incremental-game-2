using UnityEngine;
using UnityEngine.Events;

public class Ship : MonoBehaviour
{
    public UnityEvent OnCollision;

    private void OnCollisionEnter(Collision collision)
    {
        WorldObjectSpawnableNew obj = collision.collider.GetComponent<WorldObjectSpawnableNew>();
        if (obj == null)
        {
            return;
        }

        obj.ReceivePlayerImpact(this);
        OnCollision?.Invoke();
    }
}
