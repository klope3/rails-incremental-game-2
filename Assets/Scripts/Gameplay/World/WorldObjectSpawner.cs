using UnityEngine;
using RNGNeeds;

public class WorldObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObjectPool asteroidPool;
    [SerializeField] private GameObjectPool resource1Pool;
    [SerializeField] private GameObjectPool resource2Pool;
    [SerializeField] private float xWidth;
    [SerializeField] private float yOrigin;
    [SerializeField] private float zOrigin;
    [SerializeField] private float baseZVelocity;
    [SerializeField] private float spawnInterval;
    [SerializeField] private ProbabilityList<int> die;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < spawnInterval)
        {
            return;
        }

        int randPick = die.PickValue();
        if (randPick > 0)
        {
            GameObjectPool poolToUse = randPick == 1 ? asteroidPool : randPick == 2 ? resource1Pool : resource2Pool;
            GameObject go = poolToUse.GetPooledObject();
            go.SetActive(true);
            go.transform.position = new Vector3(Random.Range(-1 * xWidth, xWidth), yOrigin, zOrigin);
            go.GetComponent<Rigidbody>().linearVelocity = Vector3.back * baseZVelocity;
        }
        timer = 0;
    }
}
