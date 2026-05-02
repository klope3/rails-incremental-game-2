using UnityEngine;
using UnityEngine.Events;

public class PlayerDamageable : MonoBehaviour
{
    [SerializeField] private ECM2.Character character;
    [SerializeField] private Collider playerCollider;
    [SerializeField] private float bumpForce;
    [SerializeField] private float stunnedDuration;
    private bool stunned;
    public UnityEvent OnDamage;

    public enum DamageType
    {
        Glitch,
        WallCollision,
    }

    public void ReceiveDamage(DamageType type, Vector3 damagePoint)
    {
        if (stunned)
        {
            return;
        }

        Vector3 bumpVec = (character.transform.position - damagePoint).normalized;
        character.AddForce(bumpVec * bumpForce, ForceMode.Impulse);
        OnDamage?.Invoke();

        StartCoroutine(CO_Stun());
    }

    private System.Collections.IEnumerator CO_Stun()
    {
        stunned = true;
        playerCollider.enabled = false;
        float initialWalkSpeed = character.maxWalkSpeed;
        character.maxWalkSpeed = 0;
        yield return new WaitForSeconds(stunnedDuration);
        character.maxWalkSpeed = initialWalkSpeed;
        stunned = false;
        playerCollider.enabled = true;
    }
}
