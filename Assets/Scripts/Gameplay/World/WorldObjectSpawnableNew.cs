using UnityEngine;

public class WorldObjectSpawnableNew : MonoBehaviour
{
    [SerializeField] private Vector2 bounds;
    [SerializeField] private float baseImpactDamage;

    private void Update()
    {
        Vector3 pos = transform.position;
        if (pos.z > 0.5f * bounds.y || pos.z < -0.5f * bounds.y || pos.x < -0.5f * bounds.x || pos.x > 0.5f * bounds.x)
        {
            Debug.Log(pos);
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
        //play destruction fx, sounds, etc.
        gameObject.SetActive(false);
    }
}
