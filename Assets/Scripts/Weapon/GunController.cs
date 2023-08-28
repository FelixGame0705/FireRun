using DG.Tweening;
using GameCrewUtils;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GunController : WeaponBase
{
    [SerializeField] private Transform _firePoint;


    protected override void Update()
    {
        base.Update();
        Flip();
        //AttackMachanism(TargetAttack);
    }

    public void SpawnBullet()
    {
        GameObject bullet = GamePlayController.Instance.GetBulletPool().GetObjectFromPool();
        bullet.transform.position = _firePoint.position;
        bullet.GetComponent<Bullet>().SetUp(TargetAttack.position - _firePoint.position);
        bullet.GetComponent<Bullet>().SetDamage(1);
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
        yield return new WaitForSecondsRealtime(WeaponConfigSetting.SpeedAttack);
        SpawnBullet();
        base.PlayerAttackStage = ATTACK_STAGE.FINISHED;
        isAttack = true;
    }

    override public void AttackMachanism(Transform target)
    {
        switch (base.PlayerAttackStage)
        {
            case ATTACK_STAGE.START:
                if (CanPerformAttackState() && target.gameObject.active == true)
                {
                    SetTargetForAttack(target);
                    //Flip();
                    if (!CheckNullTarget())
                    {
                        WeaponAnimator.enabled = false;
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
