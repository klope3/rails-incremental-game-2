using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RectHazard : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;
    [HideInInspector] public Vector3 moveDirection;
    [SerializeField] private float minLifetime;
    [SerializeField] private float maxTimeOutOfBounds;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 moveVec = moveDirection * speed;
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
