using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BladeController : WeaponBase
{
    private void MoveToEnemy(Transform target)
    {
        body.transform.DOLocalMove(Vector3.zero, 1f);
        Vector3 newTarget = target.position + new Vector3(GetHalfSizeXColliderEnemy() * rotateDirectionX, GetHalfSizeYColliderEnemy() * rotateDirectionY, 0f);
        float distance = Vector3.Distance(newTarget, transform.position);
        transform.DOMove(newTarget, weaponConfig.SpeedMoveToTarget * distance);
        SetStateAttacking(ATTACK_DURATION.START, ATTACK_DURATION.DURATION);
    }

    public float GetHalfSizeXColliderEnemy()
    {
        return targetAttack.GetComponent<BoxCollider2D>().size.x/2;
    }

    public float GetHalfSizeYColliderEnemy()
    {
        return targetAttack.GetComponent<BoxCollider2D>().size.y/2;
    }


    override public bool CanPerformAttackState()
    {
        if (isStates[(int)attackDuration])
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
        animator.Play("Attack");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
        SetStateAttacking(ATTACK_DURATION.DURATION, ATTACK_DURATION.FINISHED);
    }

    private void AttackAnimation()
    {
        StartCoroutine(CheckFinishAttack());
    }

    private void EndAttack()
    {
        transform.DOLocalMove(Vector3.zero, weaponConfig.SpeedMoveToReturn).OnComplete(() => SetStateAttacking(ATTACK_DURATION.END, ATTACK_DURATION.START));
    }

    override public void AttackMachanism(Transform target)
    {
        switch (attackDuration)
        {
            case ATTACK_DURATION.START:
                if (CanPerformAttackState())
                {

                    SetTargetForAttack(target);
                    Flip();
                    if (!CheckNullTarget())
                    {
                        animator.enabled = false;
                        MoveToEnemy(targetAttack);
                        attackDuration = ATTACK_DURATION.DURATION;
                    }
                }
                break;
            case ATTACK_DURATION.DURATION:
                if (CanPerformAttackState())
                {
                    animator.enabled = true;
                    AttackDuration();
                    attackDuration = ATTACK_DURATION.FINISHED;
                }
                break;
            case ATTACK_DURATION.FINISHED:
                if (CanPerformAttackState() && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    animator.Play("Idle");
                    SetStateAttacking(ATTACK_DURATION.FINISHED, ATTACK_DURATION.END);
                    attackDuration = ATTACK_DURATION.END;
                }
                break;
            case ATTACK_DURATION.END:
                if (CanPerformAttackState())
                {
                    EndAttack();
                    attackDuration = ATTACK_DURATION.START;
                }
                break;
        }
    }
}
