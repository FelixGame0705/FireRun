using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public List<WeaponBase> weapons;

    public void ExcuteAttack(Transform target, bool isCanAttack)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            Debug.Log(weapons[i].CanPerformAttack());
            weapons[i].SetTargetForAttack(target);
            if ((isCanAttack && weapons[i].CanPerformAttackState()) || weapons[i].attackDuration != ATTACK_DURATION.START)
            {
                weapons[i].AttackMachanism(target);
            }
        }
    }
}
