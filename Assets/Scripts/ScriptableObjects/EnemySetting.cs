using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyConfig", order = 1)]
public class EnemySetting : ScriptableObject
{
    public int MaxHealth
    {
        get{ return maxHealth;}
        set{ maxHealth = value; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public float DamageAttack
    {
        get { return damageAttack; }
        set { damageAttack = value; }
    }

    public float SpeedAttack
    {
        get { return speedAttack; }
        set { speedAttack = value; }
    }

    public ATTACK_TYPE TypeAttack
    {
        get { return typeAttack; }
        set { typeAttack = value; }
    }

    [SerializeField] private int maxHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damageAttack;
    [SerializeField] private float speedAttack;
    [SerializeField] private ATTACK_TYPE typeAttack;
}
