using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponConfig", order = 1)]
public class WeaponConfig : ScriptableObject
{
    public float RangeEnemyAttack
    {
        get{ return rangeFindEnemyAttack; }
        set{ this.rangeFindEnemyAttack = value; }
    }

    public float SpeedMoveToTarget
    {
        get { return speedMoveToTarget; }
        set { this.speedMoveToTarget = value; }
    }

    public float SpeedMoveToReturn
    {
        get { return speedMoveReturn; }
        set { this.speedMoveReturn = value; }
    }

    public float RangeAttack
    {
        get { return rangAttack; }
        set { rangAttack = value; }
    }

    public List<float> DamageAttack
    {
        get { return damageAttack; }
        set { damageAttack = value; }
    }

    public float SpeedAttack
    {
        get { return speedAttack; }
        set { speedAttack = value; }
    }

    public List<DAMAGE_TYPE> TypeDamage
    {
        get { return typeDamage; }
        set { typeDamage = value; }
    }

    public RuntimeAnimatorController AnimatorWeapon
    {
        get { return animatorWeapon; }
        set { animatorWeapon = value; }
    }

    public float AngularVelocityReturn
    {
        get { return angularVelocityReturn; }
        set { angularVelocityReturn = value; }
    }

    public DamageTypeDictionary DamageTypes
    {
        set { damageTypes = value; }
        get { return damageTypes; }
    }

    public WeaponSpriteType WeaponSprites
    {
        set { weaponSprites = value; }
        get { return weaponSprites; }
    }

    [SerializeField] private WeaponSpriteType weaponSprites;
    [Tooltip("Range for finding enemy to attack")]    
    [SerializeField] private float rangeFindEnemyAttack = 3f;
    [SerializeField] private float rangAttack = 2f;
    [Tooltip("Damage attack")]
    [SerializeField] private List<float> damageAttack;
    [Tooltip("Speed attack")]
    [SerializeField] private float speedAttack;
    [Tooltip("List type damage for attacking")]
    [SerializeField] private List<DAMAGE_TYPE> typeDamage;
    [Tooltip("Animation of weapon, base on weapon")]
    [SerializeField] private RuntimeAnimatorController animatorWeapon;
    [Tooltip("Speed move to target")]
    [SerializeField] private float speedMoveToTarget = 0.2f;
    [Tooltip("Speed return local position (0,0,0)")]
    [SerializeField] private float speedMoveReturn = 0.5f;
    [Tooltip("Angular velocity to original angle")]
    [SerializeField] private float angularVelocityReturn = 5f;
    [Tooltip("Damage types and value")]
    [SerializeField] private DamageTypeDictionary damageTypes;
}
