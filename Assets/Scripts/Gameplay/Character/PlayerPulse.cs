using UnityEngine;

public class PlayerPulse : MonoBehaviour
{
    [SerializeField] private PlayerPulseWave pulseWave;
    [SerializeField] private float expansionRate;
    [SerializeField] private float duration;

    public void DoPulse()
    {
        if (pulseWave.IsPulsing)
        {
            return;
        }

        pulseWave.DoExpand(expansionRate, duration);
        pulseWave.transform.parent.position = transform.position;
    }
}
