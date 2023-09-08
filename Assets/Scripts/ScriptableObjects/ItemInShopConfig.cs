using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemConfig", order = 1)]
public class ItemInShopConfig : ScriptableObject
{
    public Sprite Sprite { get => _sprite; set => _sprite = value; }
    public ITEM_TYPE ItemType { get => _itemType; set => _itemType = value; }
    public int MaxHP { get => _maxHP; set => _maxHP = value; }
    public int HpRegeneration { get => _hpRegeneration; set => _hpRegeneration = value; }
    public int LifeSteal { get => _lifeSteal; set => _lifeSteal = value; }
    public float CritChance { get => _critChance; set => _critChance = value; }
    public DamageTypeDictionary DamageTypes { get => damageTypes; set => damageTypes = value; }
    public float RangedAttack { get => _rangedAttack; set => _rangedAttack = value; }
    public float AttackedSpeed { get => _attackedSpeed; set => _attackedSpeed = value; }
    public int Armor { get => _armor; set => _armor = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public int Harvesting { get => _harvesting; set => _harvesting = value; }
    public RARE_TYPE RareType { get=>_rareType; set => _rareType = value; }
    public string Description { get => _description; set => _description = value; }

    [SerializeField] private Sprite _sprite;
    [SerializeField] private ITEM_TYPE _itemType;
    [SerializeField] private RARE_TYPE _rareType;
    [SerializeField] private int _maxHP;
    [SerializeField] private int _hpRegeneration;
    [SerializeField] private int _lifeSteal;//hpRegeneration when attack enemy
    [SerializeField] private float _critChance;
    [Tooltip("Damage types and value")]
    [SerializeField] private DamageTypeDictionary damageTypes;
    [SerializeField] private float _rangedAttack;
    [SerializeField] private float _attackedSpeed;
    [SerializeField] private int _armor;// decrease rate damaged from enemy
    [SerializeField] private float _speed;
    [SerializeField] private int _harvesting;// ranged to eat item
    [Multiline]
    [SerializeField] private string _description;
}
