using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEnergyUI : MonoBehaviour
{
    [SerializeField] private PlayerEnergy playerEnergy;
    [SerializeField] private Image bar;
    [SerializeField] private TextMeshProUGUI numeratorText;
    [SerializeField] private TextMeshProUGUI denominatorText;

    private void Update()
    {
        bar.fillAmount = playerEnergy.CurrentAmount / playerEnergy.MaxAmount;
        numeratorText.text = $"{(int)playerEnergy.CurrentAmount}";
        denominatorText.text = $"{(int)playerEnergy.MaxAmount}";
    }
}
