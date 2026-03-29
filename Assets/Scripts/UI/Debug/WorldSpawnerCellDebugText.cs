using UnityEngine;
using TMPro;

public class WorldSpawnerCellDebugText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(string s)
    {
        text.text = s;
    }
}
