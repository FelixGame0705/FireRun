using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayController : Singleton<GamePlayController>
{
    [SerializeField] private STATE_OF_GAME _stateOfGame;
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerUpDownController _playerUpDown;
    //[SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private EnemyPool _enemiesPool;
    [SerializeField] private ObjectPool _bulletPool;
    [SerializeField] private WaveTimeController _waveTimeController;
    
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

    public EnemyPool GetEnemiesPool()
    {
        return _enemiesPool;
    }

    public ObjectPool GetBulletPool()
    {
        return _bulletPool;
    }

    public void SetStateOfGame(STATE_OF_GAME stateOfGame)
    {
        _stateOfGame = stateOfGame;
    }

    public void UpdateStateGame(STATE_OF_GAME stateOfGame)
    {
        switch (stateOfGame)
        {
            case STATE_OF_GAME.START:
                //SceneManager.LoadScene("Menu");
                break;
            case STATE_OF_GAME.PLAYING:
                //SceneManager.LoadScene("GamePlay");
                //Time.timeScale = 1;
                SetTimeEachWave();
                LimitNumeberEnemy();
                StartCoroutine(Countdown());
                break;
            case STATE_OF_GAME.PAUSE:
                Time.timeScale = 0;
                break;
            case STATE_OF_GAME.SHOPPING:
                wave += 1;
                Time.timeScale = 0;
                break;
            case STATE_OF_GAME.END:
                break;
        }
    }

    [SerializeField] private int wave = 0;
    private void SetDifficultGame()
    {
        Debug.Log("Wave: " + wave);
        _enemiesPool.GetEnemySpawn(0, wave+1);
        //SpawnEnemy();
    }

    private int minEnemySpawn = 0;
    private int maxEnemySpawn = 0;
    private void LimitNumeberEnemy()
    {
        if(wave < 3)
        {
            minEnemySpawn = 1;
            maxEnemySpawn = 5;
        }else if(wave < 7)
        {
            minEnemySpawn = 3;
            maxEnemySpawn = 8;
        }else if(wave < 12)
        {
            minEnemySpawn = 5;
            maxEnemySpawn = 10;
        }
        else
        {
            minEnemySpawn = 10;
            maxEnemySpawn = 15;
        }
    }

    //Spawn number of enemy
    private void RandomSpawnEnemy()
    {
        int random = Random.Range(minEnemySpawn, maxEnemySpawn);
        for(int i = random; i > 0; i--)
        {
            SetDifficultGame();
            SpawnEnemy();
        }
    }

    [SerializeField] private float _timeWave = 0f;
    private void SetTimeEachWave()
    {
        if(wave < 1)
        {
            _timeWave = 30f;
        }
        else if(wave < 4)
        {
            _timeWave = 60f;
        }
        else if(wave < 10)
        {
            _timeWave = 90f;
        }
        else
        {
            _timeWave = 120f;
        }
        _waveTimeController.SetCoundownTime(_timeWave);
    }

    float currentTime = 5f;
    private IEnumerator Countdown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentTime--;
        }
        RandomSpawnEnemy();
        currentTime = 5f;
        StartCoroutine(Countdown());    
    }
}
