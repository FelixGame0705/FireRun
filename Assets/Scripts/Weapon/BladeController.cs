using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BladeController : WeaponBase
{
    private void MoveToEnemy(Transform target)
    {
        Body.transform.DOLocalMove(Vector3.zero, 1f);
        Vector3 newTarget = target.position + new Vector3(GetHalfSizeXColliderEnemy() * RotateDirectionX, GetHalfSizeYColliderEnemy() * RotateDirectionY, 0f);
        float distance = Vector3.Distance(newTarget, transform.position);
        transform.DOMove(newTarget, WeaponConfigSetting.SpeedMoveToTarget * distance);
        SetStateAttacking(ATTACK_STAGE.START, ATTACK_STAGE.DURATION);
    }

    public float GetHalfSizeXColliderEnemy()
    {
        return TargetAttack.GetComponent<BoxCollider2D>().size.x/2;
    }

    public float GetHalfSizeYColliderEnemy()
    {
        return TargetAttack.GetComponent<BoxCollider2D>().size.y/2;
    }

    private void DealWithDamageToEnemy()
    {

    }

    override public bool CanPerformAttackState()
    {
        if (CheckIsAttack(PlayerAttackStage))
            return true;
        return false;
    }

    private IEnumerator CheckFinishAttack()
    {
        WeaponAnimator.Play("Attack");
        yield return new WaitUntil(() => WeaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
        SetStateAttacking(ATTACK_STAGE.DURATION, ATTACK_STAGE.FINISHED);
    }

    private void AnimateAttack()
    {
        StartCoroutine(CheckFinishAttack());
    }

    private void EndAttack()
    {
        transform.DOLocalMove(Vector3.zero, WeaponConfigSetting.SpeedMoveToReturn).OnComplete(() => SetStateAttacking(ATTACK_STAGE.END, ATTACK_STAGE.START));
    }

    override public void AttackMachanism(Transform target)
    {
        switch (base.PlayerAttackStage)
        {
            case ATTACK_STAGE.START:
                if (CanPerformAttackState())
                {

                    SetTargetForAttack(target);
                    Flip();
                    if (!CheckNullTarget())
                    {
                        WeaponAnimator.enabled = false;
                        MoveToEnemy(TargetAttack);
                        base.PlayerAttackStage = ATTACK_STAGE.DURATION;
                    }
                }
                break;
            case ATTACK_STAGE.DURATION:
                if (CanPerformAttackState())
                {
                    Debug.Log("Attack duration");
                    WeaponAnimator.enabled = true;
                    AnimateAttack();
                    TargetAttack.GetComponent<Enemy>().TakeDamage(1);
                    //DamageEnemy();
                    base.PlayerAttackStage = ATTACK_STAGE.FINISHED;
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
                    EndAttack();
                    base.PlayerAttackStage = ATTACK_STAGE.START;
                }
                break;
        }
    }
}
