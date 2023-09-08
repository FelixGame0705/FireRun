using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool
{
    [SerializeField] private List<GameObject> _enemyPool;

    public GameObject GetEnemySpawn(int min, int max)
    {
        int random = Random.Range(min, max);
        objectPrefab = _enemyPool[random];
        return objectPrefab;
    }
}
