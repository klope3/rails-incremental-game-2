using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergyUI : MonoBehaviour
{
    [SerializeField] private PlayerEnergy playerEnergy;
    [SerializeField] private Image bar;

    private void Update()
    {
        bar.fillAmount = playerEnergy.CurrentAmount / playerEnergy.MaxAmount;
    }
}
