using UnityEngine;
using Sirenix.OdinInspector;

public class GameDebug : MonoBehaviour
{
    [SerializeField] private bool disablePlayer;

    [SerializeField] private bool giveStartingAbility;
    [SerializeField, ShowIf("@giveStartingAbility")] private AbilitySO startingAbility;

    [SerializeField] private bool giveStartingResources;
    [SerializeField, ShowIf("@giveStartingResources")] private int resource1Amount;
    [SerializeField, ShowIf("@giveStartingResources")] private int resource2Amount;
    [SerializeField, ShowIf("@giveStartingResources")] private int resource3Amount;
    [SerializeField, ShowIf("@giveStartingResources")] private ResourceSO resource1;
    [SerializeField, ShowIf("@giveStartingResources")] private ResourceSO resource2;
    [SerializeField, ShowIf("@giveStartingResources")] private ResourceSO resource3;

    [SerializeField] private bool preventEnergyDrain;
    [SerializeField] private bool disableShipControl;

    [SerializeField] private GameObject player;
    [SerializeField] private AbilityHandler abilityHandler;
    [SerializeField] private PlayerEnergy playerEnergy;
    [SerializeField] private ShipControl shipControl;
    [SerializeField] private ResourceHandler resourceHandler;

    public void Initialize()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        Debug.Log("===============APPLYING DEBUG SETTINGS===============");

        if (disablePlayer)
        {
            player.SetActive(false);
        }
        
        if (giveStartingAbility)
        {
            abilityHandler.SetAbility(startingAbility);
        }

        if (preventEnergyDrain)
        {
            playerEnergy.EnergyCanDrain = false;
        }

        if (disableShipControl)
        {
            shipControl.enabled = false;
        }

        if (giveStartingResources)
        {
            AddStartingResources();
        }
    }

    [Button]
    private void DepleteEnergyNow()
    {
        playerEnergy.Add(float.MinValue);
    }

    [Button]
    private void AddStartingResources()
    {
        resourceHandler.AddResource(resource1, resource1Amount, true);
        resourceHandler.AddResource(resource2, resource2Amount, true);
        resourceHandler.AddResource(resource3, resource3Amount, true);
    }

    [Button]
    private void ReinitializeGameDatabase()
    {
        GameDatabase.Reinitialize();
    }
}
