using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private List<WeaponBase> weapons;

    public void ExcuteAttack(Transform target, bool isCanAttack)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            Debug.Log(weapons[i].CanPerformAttack());
            weapons[i].SetTargetForAttack(target);
            if ((isCanAttack && weapons[i].CanPerformAttackState()) || weapons[i].CheckCurrentStateActive(ATTACK_STAGE.START) == false)
            {
                weapons[i].AttackMachanism(target);
            }
        }
    }

    public List<WeaponBase> GetWeapons()
    {
        return weapons;
    }

    public WeaponBase GetWeapon(int index)
    {
        return weapons[index];
    }
}
