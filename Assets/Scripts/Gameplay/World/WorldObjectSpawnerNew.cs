using UnityEngine;
using RNGNeeds;

public class WorldObjectSpawnerNew : MonoBehaviour
{
    [SerializeField] private Vector2 bounds;
    [SerializeField] private float yOffset;
    [SerializeField] private float outerBoundsOffset;
    [SerializeField] private float spawnAttemptInterval;
    [SerializeField] private float launchForce;
    [SerializeField] private GameObjectPool asteroidPool;
    [SerializeField] private GameObjectPool resource1Pool;
    [SerializeField] private GameObjectPool resource2Pool;
    [SerializeField] private ProbabilityList<int> die;
    private float timer;

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
        int randPick = die.PickValue();
        if (randPick == 0)
        {
            return;
        }

        if (randPick == 1)
        {
            Vector3 randPos = GetRandomOuterBoundsPosition();
            Vector3 randVec = GetRandomInwardVector(randPos);
            GameObject go = asteroidPool.GetPooledObject();
            go.SetActive(true);
            go.transform.position = randPos;
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(randVec * launchForce, ForceMode.Impulse);
        }

        if (randPick == 2)
        {
            GameObject go = resource1Pool.GetPooledObject();
            go.SetActive(true);
            go.transform.position = GetRandomWithinBoundsPosition();
        }

        if (randPick == 3)
        {
            GameObject go = resource2Pool.GetPooledObject();
            go.SetActive(true);
            go.transform.position = GetRandomWithinBoundsPosition();
        }
    }

    public void DeactivateAllObjects()
    {
        asteroidPool.DeactivateAll();
        resource1Pool.DeactivateAll();
        resource2Pool.DeactivateAll();
    }

    private Vector3 GetRandomWithinBoundsPosition()
    {
        float randX = Random.Range(-0.5f * bounds.x, 0.5f * bounds.x);
        float randZ = Random.Range(-0.5f * bounds.y, 0.5f * bounds.y);
        Vector3 randPos = new Vector3(randX, yOffset, randZ);
        return randPos;
    }

    //get a random position that's just outside the bounds
    private Vector3 GetRandomOuterBoundsPosition()
    {
        Vector3 randPos = GetRandomWithinBoundsPosition();
        int randShiftChoice = Random.Range(0, 4);

        if (randShiftChoice == 0)
        {
            randPos.x = -0.5f * bounds.x - outerBoundsOffset;
        } else if (randShiftChoice == 1)
        {
            randPos.z = 0.5f * bounds.y + outerBoundsOffset;
        } else if (randShiftChoice == 2)
        {
            randPos.x = 0.5f * bounds.x + outerBoundsOffset;
        } else
        {
            randPos.z = -0.5f * bounds.y - outerBoundsOffset;
        }

        return randPos;
    }

    //get a random vector that's guaranteed to be facing at least somewhat toward the world origin, starting from the given position
    private Vector3 GetRandomInwardVector(Vector3 position)
    {
        Vector3 randAimPoint = GetRandomWithinBoundsPosition();
        return (randAimPoint - position).normalized;
    }
}
