using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RectHazard : MonoBehaviour
{
    [SerializeField] private Vector2 vel;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 moveVec = Vector3.right * vel.x + Vector3.forward * vel.y;
        rb.MovePosition(transform.position + moveVec * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerDamageable damageable = collision.collider.GetComponent<PlayerDamageable>();
        if (damageable != null)
        {
            damageable.ReceiveDamage(PlayerDamageable.DamageType.Glitch, collision.GetContact(0).point);
        }
    }
}
