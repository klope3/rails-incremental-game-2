using UnityEngine;

public class GameObjectPoolHelper : MonoBehaviour
{
    [SerializeField] private GameObjectPool[] pools;

    public GameObject PickRandomObject()
    {
        if (pools.Length == 0) return null;

        int randIndex = Random.Range(0, pools.Length);
        GameObjectPool randPool = pools[randIndex];
        return randPool.GetPooledObject();
    }
}
