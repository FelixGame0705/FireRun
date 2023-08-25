using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _game;
    //[SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<GameObject> _enemies;
    //SerializedDictionary<WEAPON_TYPE, string> _weaponTypes;
    //[SerializeField] private 
    // Start is called before the first frame update
    void Start()
    {
        
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

    private GameObject SpawnEnemy(GameObject enemy)
    {
        GameObject enemySpawning = Instantiate(enemy, GetRandomPositionSpawn(), Quaternion.identity) as GameObject;
        _enemies.Add(enemySpawning);
        return enemySpawning;
    }
}
