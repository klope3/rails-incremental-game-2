using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RNGNeeds;

public class WorldObjectSpawnerNew2 : MonoBehaviour
{
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
            GameWorld.Direction randBorder = GameWorld.PickRandomDirection();
            GameWorld.Direction moveDirection = GameWorld.InvertDirection(randBorder);
            Vector3 randBorderPos = GameWorld.Instance.GetRandomBorderPosition(randBorder);
            go.transform.position = randBorderPos;
            go.GetComponent<RectHazard>().moveDirection = GameWorld.DirectionToVector(moveDirection);
        }
    }

    private List<Vector3> GetPositionsBeyondDistFromPlayer(float distance)
    {
        return GameWorld.Instance.GetAllGridPositions().Where(pos => Vector3.Distance(pos, playerTransform.position) > distance).ToList();
    }

    public void DeactivateAllObjects()
    {
        resource1Pool.DeactivateAll();
        glitchPool.DeactivateAll();
    }
}
