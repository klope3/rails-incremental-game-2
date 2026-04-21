using UnityEngine;
using UnityEngine.Events;

public class GameFlow : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private PlayerEnergy playerEnergy;
    [SerializeField] private ShipControl shipControl;
    [SerializeField] private ResourceRankHandler rankHandler;
    [SerializeField] private WorldObjectSpawnerNew spawner;
    public UnityEvent OnPlayModeActive;
    public UnityEvent OnUpgradeModeActive;

    public void Initialize()
    {
        SetPlayMode();
        playerEnergy.OnDepleted += PlayerEnergy_OnDepleted;
    }

    private void PlayerEnergy_OnDepleted()
    {
        SetUpgradeMode();
    }

    public void SetPlayMode()
    {
        playerObject.SetActive(true);
        shipControl.PrepareForLevel();
        playerEnergy.PrepareForLevel();
        rankHandler.PrepareForLevel();
        spawner.enabled = true;

        OnPlayModeActive?.Invoke();
    }

    public void SetUpgradeMode()
    {
        playerObject.SetActive(false);
        spawner.DeactivateAllObjects();
        spawner.enabled = false;

        OnUpgradeModeActive?.Invoke();
    }

    public void SetPauseMode(bool paused)
    {
        Time.timeScale = paused ? 0 : 1;
    }
}
