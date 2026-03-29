using UnityEngine;

public class WorldObjectSpawnable : MonoBehaviour
{
    [SerializeField] private float baseImpactDamage;
    [field: SerializeField, Tooltip("How many cells on the spawning grid this object will take up.")] 
    public Vector2Int CellDimensions { get; private set; }
    [SerializeField, Tooltip("The object will be deactivated when it passes this Z-coordinate.")] 
    private float minZ;

    private void OnDrawGizmos()
    {
        float centerX = transform.position.x + CellDimensions.x * 0.5f - 0.5f;
        float centerZ = transform.position.z + CellDimensions.y * 0.5f - 0.5f;
        Vector3 center = new Vector3(centerX, 0, centerZ);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, new Vector3(CellDimensions.x, 1, CellDimensions.y));
    }

    private void Update()
    {
        if (transform.position.z < minZ)
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
        //play destruction fx, sounds, etc.
        gameObject.SetActive(false);
    }
}
