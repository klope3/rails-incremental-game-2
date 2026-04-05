using UnityEngine;
using UnityEngine.Events;

public class WorldObjectSpawnableNew : MonoBehaviour
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
        if (baseImpactDamage > 0)
        {
            PlayerEnergy energy = playerShip.GetComponent<PlayerEnergy>();
            if (energy != null)
            {
                energy.Add(-1 * baseImpactDamage);
            }
        }
        DoDestroy(); //this will eventually be more nuanced
    }

    private void DoDestroy()
    {
        OnDestroy?.Invoke();
        StartCoroutine(CO_Disable());
    }

    private System.Collections.IEnumerator CO_Disable()
    {
        yield return new WaitForSeconds(deactivateDelay);
        gameObject.SetActive(false);
    }
}
