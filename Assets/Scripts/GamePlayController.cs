using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : Singleton<GamePlayController>
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _game;
    //[SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private ObjectPool _enemiesPool;
    [SerializeField] private ObjectPool _bulletPool;
    //SerializedDictionary<WEAPON_TYPE, string> _weaponTypes;
    //[SerializeField] private 
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        SpawnEnemy();
        //StartCoroutine(WaitSpawnPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector2 GetRandomPositionSpawn()
    {
        Vector2 randomSpawnPos = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        return randomSpawnPos;
    }

    IEnumerator WaitSpawnPlayer()
    {
        yield return new WaitUntil(() => SpawnPlayer());
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        _enemiesPool.GetObjectFromPool().GetComponent<Enemy>().SetTargetPlayer(_player);
    }

    private bool SpawnPlayer()
    {
        GameObject playerSpawning = Instantiate(_player, GetRandomPositionSpawn(), Quaternion.identity);
        return true;
    }

    public ObjectPool GetEnemiesPool()
    {
        return _enemiesPool;
    }

    public ObjectPool GetBulletPool()
    {
        return _bulletPool;
    }
}
