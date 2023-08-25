using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySetting _enemyConfig;
    [SerializeField] private Collider2D _collider2D;

    private float _currentHealth;
    private float _maxHealth;
    private Transform _playerUpDownController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
