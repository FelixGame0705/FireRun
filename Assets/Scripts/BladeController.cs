using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BladeController : WeaponBase
{
    private void MoveToEnemy(Transform target)
    {
        Body.transform.DOLocalMove(Vector3.zero, 1f);
        Vector3 newTarget = target.position + new Vector3(GetHalfSizeXColliderEnemy() * rotateDirectionX, GetHalfSizeYColliderEnemy() * rotateDirectionY, 0f);
        float distance = Vector3.Distance(newTarget, transform.position);
        transform.DOMove(newTarget, WeaponConfigSetting.SpeedMoveToTarget * distance);
        SetStateAttacking(ATTACK_DURATION.START, ATTACK_DURATION.DURATION);
    }

    public float GetHalfSizeXColliderEnemy()
    {
        return TargetAttack.GetComponent<BoxCollider2D>().size.x/2;
    }

    public float GetHalfSizeYColliderEnemy()
    {
        return TargetAttack.GetComponent<BoxCollider2D>().size.y/2;
    }


    override public bool CanPerformAttackState()
    {
        if (isStates[(int)base.AttackDuration])
            return true;
        return false;
    }

    override public void SetStateAttacking(ATTACK_DURATION currentState, ATTACK_DURATION stateContinue)
    {
        isStates[(int)currentState] = false;
        isStates[(int)stateContinue] = true;
    }

    private void AttackDuration()
    {
        if (CanPerformAttackState())
        {
            AttackAnimation();
        }
    }

    IEnumerator CheckFinishAttack()
    {
        WeaponAnimator.Play("Attack");
        yield return new WaitUntil(() => WeaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
        SetStateAttacking(ATTACK_DURATION.DURATION, ATTACK_DURATION.FINISHED);
    }

    private void AttackAnimation()
    {
        StartCoroutine(CheckFinishAttack());
    }

    private void EndAttack()
    {
        transform.DOLocalMove(Vector3.zero, WeaponConfigSetting.SpeedMoveToReturn).OnComplete(() => SetStateAttacking(ATTACK_DURATION.END, ATTACK_DURATION.START));
    }

    override public void AttackMachanism(Transform target)
    {
        switch (base.AttackDuration)
        {
            case ATTACK_DURATION.START:
                if (CanPerformAttackState())
                {

                    SetTargetForAttack(target);
                    Flip();
                    if (!CheckNullTarget())
                    {
                        WeaponAnimator.enabled = false;
                        MoveToEnemy(TargetAttack);
                        base.AttackDuration = ATTACK_DURATION.DURATION;
                    }
                }
                break;
            case ATTACK_DURATION.DURATION:
                if (CanPerformAttackState())
                {
                    WeaponAnimator.enabled = true;
                    AttackDuration();
                    base.AttackDuration = ATTACK_DURATION.FINISHED;
                }
                break;
            case ATTACK_DURATION.FINISHED:
                if (CanPerformAttackState() && WeaponAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    WeaponAnimator.Play("Idle");
                    SetStateAttacking(ATTACK_DURATION.FINISHED, ATTACK_DURATION.END);
                    base.AttackDuration = ATTACK_DURATION.END;
                }
                break;
            case ATTACK_DURATION.END:
                if (CanPerformAttackState())
                {
                    EndAttack();
                    base.AttackDuration = ATTACK_DURATION.START;
                }
                break;
        }
    }
}
