using UnityEngine;

public class AbilityUseContext
{
    public PlayerDash PlayerDash { get; private set; }
    public PlayerPulse PlayerPulse { get; private set; }
    public PlayerLaser PlayerLaser { get; private set; }

    public AbilityUseContext(PlayerDash playerDash, PlayerPulse playerPulse, PlayerLaser playerLaser)
    {
        PlayerDash = playerDash;
        PlayerPulse = playerPulse;
        PlayerLaser = playerLaser;
    }
}
