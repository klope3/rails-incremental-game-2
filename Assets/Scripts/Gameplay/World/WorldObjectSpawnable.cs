using UnityEngine;

public class WorldObjectSpawnable : MonoBehaviour
{
    [SerializeField] private float baseImpactDamage;
    [SerializeField, Tooltip("The object will be deactivated when it passes this Z-coordinate.")] 
    private float minZ;

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
