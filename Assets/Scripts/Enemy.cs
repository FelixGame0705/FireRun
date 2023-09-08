using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySetting _enemyConfig;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private GameObject _targetPlayer;
    [SerializeField] public Animator _animator;
    [SerializeField] private int _currentHealth;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private int _maxHealth;
    private Transform _playerUpDownController;

    private ATTACK_STAGE _attackStage = ATTACK_STAGE.START;
    private Dictionary<DAMAGE_TYPE, int> _damageTypes = new Dictionary<DAMAGE_TYPE, int>();
    // Start is called before the first frame update
    void Start()
    {
        _maxHealth = _enemyConfig.MaxHealth;
        _currentHealth = _maxHealth;
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
        if (isHurt)
        {
            StartCoroutine(DelayHurt());
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

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0) GamePlayController.Instance.GetEnemiesPool().ReturnObjectToPool(gameObject);
    }

    public void TakeDamage(DAMAGE_TYPE damageType, int damage)
    {
        _damageTypes.Add(damageType, damage);
    }

    IEnumerator BurnDamage()
    {
        _currentHealth -= _damageTypes[DAMAGE_TYPE.FIRE];
        yield return new WaitForSeconds(3f);
        _damageTypes.Remove(DAMAGE_TYPE.FIRE);
    }

    IEnumerator PoisonDamage()
    {
        _currentHealth -= _damageTypes[DAMAGE_TYPE.POISON];
        yield return new WaitForSeconds(3f);
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public void SetTargetPlayer(GameObject target)
    {
        _targetPlayer = target;
    }

    protected virtual void AttackMechanism()
    {
        switch (_attackStage)
        {
            case ATTACK_STAGE.START:
                //MoveToPlayer();
                if(CanPerformAttack())
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

    public void Hurt()
    {
        _spriteRenderer.material.color = Color.red;
    }

    public bool isHurt = true;
    public IEnumerator DelayHurt()
    {
        isHurt = false;
        Hurt();
        yield return new WaitForSeconds(1f);
        _spriteRenderer.material.color = Color.white;
    }
}
