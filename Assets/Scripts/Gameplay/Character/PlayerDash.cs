using UnityEngine;
using ECM2;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ShipControl shipControl;
    [SerializeField] private Character character;
    [SerializeField] private float force;
    [SerializeField] private float drag;
    [SerializeField] private float dragDelay;
    [SerializeField] private float dashMaxTime;
    private float initialDamping;
    public bool IsDashing { get; private set; }
    private float dashTimer;
    public bool IsPowerful { get; private set; } //true when the player is doing a "power dash"

    private void Update()
    {
        if (!IsDashing)
        {
            return;
        }
        dashTimer += Time.deltaTime;

        if (dashTimer > dragDelay)
        {
            rb.linearDamping = drag;
        }

        if (dashTimer > dashMaxTime)
        {
            StopDash();
        }
    }

    public void DoDash(bool powerful = false)
    {
        if (IsDashing)
        {
            return;
        }

        rb.isKinematic = false;
        character.enabled = false;
        initialDamping = rb.linearDamping;
        rb.linearDamping = 0;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        rb.AddForce(shipControl.MoveVec * force, ForceMode.Impulse);
        IsDashing = true;
        IsPowerful = powerful;
        dashTimer = 0;
    }

    private void StopDash()
    {
        rb.isKinematic = true;
        character.enabled = true;
        initialDamping = rb.linearDamping;
        rb.linearDamping = initialDamping;
        IsDashing = false;
        IsPowerful = false;
    }
}
