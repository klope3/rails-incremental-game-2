using UnityEngine;

public class WorldSpawnerCellDebug : MonoBehaviour
{
    [SerializeField] private WorldSpawnerCellDebugText debugPf;
    [SerializeField] private WorldObjectSpawner spawner;

    private void Awake()
    {
        for (int i = 0; i < spawner.CellsWidth; i++)
        {
            WorldSpawnerCellDebugText newText = Instantiate(debugPf, transform);
            newText.transform.position = spawner.CellToWorldPosition(i);
        }
    }

    private void Update()
    {
        var counters = spawner.CellOccupancyCounters;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);
            WorldSpawnerCellDebugText text = t.GetComponent<WorldSpawnerCellDebugText>();
            text.SetText($"{counters[i]}");
        }
    }
}
