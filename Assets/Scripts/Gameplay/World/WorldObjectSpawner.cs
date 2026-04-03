using System.Collections.Generic;
using UnityEngine;
using RNGNeeds;

public class WorldObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObjectPool asteroidPool;
    [SerializeField] private GameObjectPool resource1Pool;
    [SerializeField] private GameObjectPool resource2Pool;
    [field: SerializeField] public int CellsWidth;
    [SerializeField] private float yOrigin;
    [SerializeField] private float zOrigin;
    [SerializeField] private float spawnInterval;
    [SerializeField] private ProbabilityList<int> die;
    [SerializeField] private float velocityZ;
    private float timer;
    private HashSet<Rigidbody> spawnedRigidbodies;
    //each int of cellOccupancyCounters represents the remaining length of the object that needs to move out of the way before that cell is considered unoccupied.
    //e.g. when an object of length 6 spawns, the counters for the cells it covers are set to 6, and then decrement at a speed which depends on how fast the level is moving.
    //when the counters reach 0 again, the object has moved out of the way, and the cells can accommodate a new object if needed.
    //the first int of the array represents the far left cell, and the last represents the far right cell.
    private int[] cellOccupancyCounters;
    public IReadOnlyList<int> CellOccupancyCounters => cellOccupancyCounters;

    private void Awake()
    {
        spawnedRigidbodies = new HashSet<Rigidbody>();
        cellOccupancyCounters = new int[CellsWidth];
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1 / velocityZ) //e.g. if the velocity is 4, then objects cover 1 unit in 0.25 seconds, so counters will be updated 4 times per second
        {
            timer = 0;
            UpdateCounters();
            HandleSpawn();
        }
    }

    private void FixedUpdate()
    {
        MoveObjects();
    }

    private void MoveObjects()
    {
        foreach (Rigidbody rb in spawnedRigidbodies)
        {
            if (rb.gameObject.activeInHierarchy)
            {
                rb.MovePosition(rb.transform.position + Vector3.back * velocityZ * Time.fixedDeltaTime);
            }
        }
    }

    public void DeactivateAllObjects()
    {
        asteroidPool.DeactivateAll();
        resource1Pool.DeactivateAll();
        resource2Pool.DeactivateAll();
    }

    private void UpdateCounters()
    {
        for (int i = 0; i < cellOccupancyCounters.Length; i++)
        {
            if (cellOccupancyCounters[i] == 0) continue;
            cellOccupancyCounters[i]--;
        }
    }

    private void HandleSpawn()
    {
        int randPick = die.PickValue();
        if (randPick == 0)
        {
            return;
        }
        int randCellIndex = PickRandomUnoccupiedCellIndex(); //only finds a single cell; if a big object is randomly picked, it still might not fit
        if (randCellIndex == -1)
        {
            return; //there are no spots left
        }

        GameObjectPool poolToUse = randPick == 1 ? asteroidPool : randPick == 2 ? resource1Pool : resource2Pool;
        GameObject go = poolToUse.GetPooledObject();
        Vector2Int cellDimensions = go.GetComponent<WorldObjectSpawnable>().CellDimensions;
        for (int i = randCellIndex; i < randCellIndex + cellDimensions.x; i++)
        {
            if (i >= cellOccupancyCounters.Length)
            {
                return; //this object is too big and would extend off the screen to the right, so the spawn fails
            }
            if (cellOccupancyCounters[i] > 0)
            {
                return; //this object is too big and would intersect another one, so the spawn fails
            }
        }
        for (int i = randCellIndex; i < randCellIndex + cellDimensions.x && i < cellOccupancyCounters.Length; i++)
        {
            cellOccupancyCounters[i] = cellDimensions.y;
        }
        go.SetActive(true);
        go.transform.position = CellToWorldPosition(randCellIndex);
        go.transform.rotation = Quaternion.identity;
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        spawnedRigidbodies.Add(rb);
    }

    private int PickRandomUnoccupiedCellIndex()
    {
        List<int> unoccupiedIndices = new List<int>();
        for (int i = 0; i < CellsWidth; i++)
        {
            if (cellOccupancyCounters[i] == 0)
            {
                unoccupiedIndices.Add(i);
            }
        }

        if (unoccupiedIndices.Count == 0)
        {
            return -1;
        }

        int randIndex = Random.Range(0, unoccupiedIndices.Count);
        return unoccupiedIndices[randIndex];
    }

    public Vector3 CellToWorldPosition(int cellIndex)
    {
        return new Vector3(cellIndex - CellsWidth / 2f, yOrigin, zOrigin);
    }
}
