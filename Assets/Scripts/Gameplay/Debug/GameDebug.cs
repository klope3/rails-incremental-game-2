using UnityEngine;
using Sirenix.OdinInspector;

public class GameDebug : MonoBehaviour
{
    [SerializeField] private bool disablePlayer;
    [SerializeField] private bool giveStartingAbility;
    [SerializeField, ShowIf("@giveStartingAbility")] private AbilitySO startingAbility;
    [SerializeField] private bool preventEnergyDrain;
    [SerializeField] private bool disableShipControl;
    [SerializeField] private GameObject player;
    [SerializeField] private AbilityHandler abilityHandler;
    [SerializeField] private PlayerEnergy playerEnergy;
    [SerializeField] private ShipControl shipControl;

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
    }
}
