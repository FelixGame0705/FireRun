using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySetting _enemyConfig;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private GameObject _targetPlayer;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _currentHealth;
    public ObjectPool ob;
    private float _maxHealth;
    private Transform _playerUpDownController;

    private ATTACK_STAGE _attackStage;
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _enemyConfig.MaxHealth;
        for (int i = 0; i < 14; i++)
            ob.GetObjectFromPool();
        var hp = FindObjectOfType<PlayerUpDownController>();
        _targetPlayer = hp.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //if(CanPerformAttack())
        if (_targetPlayer != null)
        {
            AttackMechanism();
            MoveToPlayer();
            Flip();
        }
    }

    private void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPlayer.transform.position, 3f * Time.deltaTime);
    }

    private IEnumerator CheckFinishedAttack()
    {
        _animator.speed = 0.5f;
        _animator.Play("Attack");
        _collider2D.enabled = true;
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
        _attackStage = ATTACK_STAGE.FINISHED;
    }

    private void Attack()
    {
        
        StartCoroutine(CheckFinishedAttack());
    }

    private bool CanPerformAttack()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return true;
        }
        return false;
    }

    private void FinishedAttack()
    {
        _animator.Play("idle");
        _collider2D.enabled = false;
        _attackStage = ATTACK_STAGE.END;
    }

    private void Flip()
    {
        transform.localScale = transform.position.x < _targetPlayer.transform.position.x?new Vector3(-1,1,1):new Vector3(1,1,1);
    }

    public void MinusCurrentHealth(int healthDamage)
    {
        _currentHealth -= healthDamage;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    protected virtual void AttackMechanism()
    {
        switch (_attackStage)
        {
            case ATTACK_STAGE.START:
                //MoveToPlayer();
                if(Vector2.Distance(transform.position, _targetPlayer.transform.position) < 1f)
                {
                    _attackStage = ATTACK_STAGE.DURATION;
                }
                break;
            case ATTACK_STAGE.DURATION:
                if(CanPerformAttack())
                Attack();
                break;
            case ATTACK_STAGE.FINISHED:
                if(CanPerformAttack())
                FinishedAttack();
                break;
            case ATTACK_STAGE.END:
                _attackStage = ATTACK_STAGE.START;
                break;
        }
    }
    
}
