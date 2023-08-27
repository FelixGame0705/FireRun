using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : WeaponBase
{
    [SerializeField] private Transform _firePoint;

    public void SpawnBullet()
    {
        GameObject bullet = GamePlayController.Instance.GetBulletPool().GetObjectFromPool();
        bullet.transform.position = _firePoint.position;
        bullet.GetComponent<Bullet>().SetUp(TargetAttack.position - _firePoint.position);
        //ReturnBulletToPool(bullet);
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        if(Vector2.Distance(bullet.transform.position, transform.position) > WeaponConfigSetting.RangeEnemyAttack)
        {
            GamePlayController.Instance.GetBulletPool().ReturnObjectToPool(bullet);
        }
    }

    public override bool CanPerformAttackState()
    {
        if (CheckIsAttack(PlayerAttackStage)) return true;
        return false;
    }

    bool isAttack = true;
    IEnumerator DelayAttack()
    {
        isAttack = false;
        SetStateAttacking(ATTACK_STAGE.DURATION, ATTACK_STAGE.FINISHED);
        yield return new WaitForSecondsRealtime(3f);
        SpawnBullet();
        
        //TargetAttack.GetComponent<Enemy>().TakeDamage(1);
        //DamageEnemy();
        base.PlayerAttackStage = ATTACK_STAGE.FINISHED;
        isAttack = true;
    }

    public override void AttackMachanism(Transform target)
    {
        switch (base.PlayerAttackStage)
        {
            case ATTACK_STAGE.START:
                if (CanPerformAttackState())
                {
                    Debug.Log("Gun Start");
                    SetTargetForAttack(target);
                    Flip();
                    if (!CheckNullTarget())
                    {
                        WeaponAnimator.enabled = false;
                        //MoveToEnemy(TargetAttack);
                        SetStateAttacking(ATTACK_STAGE.START, ATTACK_STAGE.DURATION);
                        base.PlayerAttackStage = ATTACK_STAGE.DURATION;
                    }
                }
                break;
            case ATTACK_STAGE.DURATION:
                if (CanPerformAttackState()&&isAttack)
                {
                    
                    WeaponAnimator.enabled = true;
                    //AnimateAttack();
                    WeaponAnimator.Play("Attack");
                    StartCoroutine(DelayAttack());  
                }
                break;
            case ATTACK_STAGE.FINISHED:
                if (CanPerformAttackState() && WeaponAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    WeaponAnimator.Play("Idle");
                    SetStateAttacking(ATTACK_STAGE.FINISHED, ATTACK_STAGE.END);
                    base.PlayerAttackStage = ATTACK_STAGE.END;
                }
                break;
            case ATTACK_STAGE.END:
                if (CanPerformAttackState())
                {
                    SetStateAttacking(ATTACK_STAGE.FINISHED, ATTACK_STAGE.START);
                    base.PlayerAttackStage = ATTACK_STAGE.START;
                    //EndAttack();
                    
                }
                break;
        }
    }
}
