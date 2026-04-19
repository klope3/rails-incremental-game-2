using UnityEngine;
using UnityEngine.Events;

public class WorldObjectSpawnableNew : MonoBehaviour, IProjectileImpactable
{
    [SerializeField] private Vector2 bounds;
    [SerializeField] private float baseImpactDamage;
    [SerializeField] private float deactivateDelay;
    public UnityEvent OnEnabled;
    public UnityEvent OnDestroy;

    private void OnEnable()
    {
        OnEnabled?.Invoke();
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        if (pos.z > 0.5f * bounds.y || pos.z < -0.5f * bounds.y || pos.x < -0.5f * bounds.x || pos.x > 0.5f * bounds.x)
        {
            gameObject.SetActive(false);
        }
    }

    public void ReceivePlayerImpact(Ship playerShip)
    {
        PlayerDash playerDash = playerShip.GetComponent<PlayerDash>();
        if (baseImpactDamage > 0)
        {
            PlayerEnergy energy = playerShip.GetComponent<PlayerEnergy>();
            if (energy != null && (playerDash == null || !playerDash.IsDashing))
            {
                energy.Add(-1 * baseImpactDamage);
            }
        }

        if (playerDash != null && playerDash.IsDashing && playerDash.IsPowerful)
        {
            DoDestroy();
        }
    }

    public void ReceivePulseImpact(PlayerPulseWave pulseWave)
    {
        DoDestroy();
    }

    private void DoDestroy()
    {
        Debug.Log("Destroy");
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        OnDestroy?.Invoke();
        StartCoroutine(CO_Disable());
    }

    private System.Collections.IEnumerator CO_Disable()
    {
        yield return new WaitForSeconds(deactivateDelay);
        gameObject.SetActive(false);
    }

    public void Impact(Projectile projectile, RaycastHit hitInfo)
    {
        DoDestroy();
    }
}
