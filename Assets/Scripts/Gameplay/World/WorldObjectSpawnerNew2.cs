using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RNGNeeds;

public class WorldObjectSpawnerNew2 : MonoBehaviour
{
    [SerializeField] private Vector2 bounds;
    [SerializeField] private float yOffset;
    [SerializeField] private GameObjectPool resource1Pool;
    [SerializeField] private GameObjectPoolHelper glitchPool;
    [SerializeField] private float spawnAttemptInterval;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float minPlayerDistance;
    [SerializeField] private ProbabilityList<SpawnType> spawnChances;
    private float timer;

    public enum SpawnType
    {
        Null,
        ResourceAlpha,
        ResourceBeta,
        RectHazard,
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(bounds.x, 1, bounds.y));
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnAttemptInterval)
        {
            Spawn();
            timer = 0;
        }
    }

    private void Spawn()
    {
        SpawnType randType = spawnChances.PickValue();

        if (randType == SpawnType.ResourceAlpha)
        {
            GameObject go = resource1Pool.GetPooledObject();
            go.SetActive(true);
            go.transform.position = Utils.PickRandomFromCollection(GetPositionsBeyondDistFromPlayer(minPlayerDistance));
        }
        if (randType == SpawnType.RectHazard)
        {
            GameObject go = glitchPool.PickRandomObject();
            go.SetActive(true);
            go.transform.position = Utils.PickRandomFromCollection(GetPositionsBeyondDistFromPlayer(minPlayerDistance));
        }
    }

    private List<Vector3> GetPositionsBeyondDistFromPlayer(float distance)
    {
        return GetAllGridPositions().Where(pos => Vector3.Distance(pos, playerTransform.position) > distance).ToList();
    }

    public void DeactivateAllObjects()
    {
        resource1Pool.DeactivateAll();
    }

    private List<Vector3> GetAllGridPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        Vector3 northwestCorner = GetNorthWestCorner();
        Vector3 southeastCorner = GetSoutheastCorner();

        for (int x = (int)northwestCorner.x; x < southeastCorner.x; x++)
        {
            for (int y = (int)northwestCorner.z; y > southeastCorner.z; y--)
            {
                positions.Add(new Vector3(x, yOffset, y));
            }
        }

        return positions;
    }

    private Vector3 GetNorthWestCorner()
    {
        return new Vector3(Mathf.Round(-0.5f * bounds.x), yOffset, Mathf.Round(0.5f * bounds.y));
    }

    private Vector3 GetSoutheastCorner()
    {
        return new Vector3(0.5f * bounds.x, yOffset, -0.5f * bounds.y);
    }

    private Vector3 GetRandomWithinBoundsPosition()
    {
        float randX = Random.Range(-0.5f * bounds.x, 0.5f * bounds.x);
        float randZ = Random.Range(-0.5f * bounds.y, 0.5f * bounds.y);
        Vector3 randPos = new Vector3(Mathf.Round(randX), yOffset, Mathf.Round(randZ));
        return randPos;
    }
}
