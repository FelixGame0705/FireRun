using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DamageTypeDictionary
{
    [SerializeField]
    List<DAMAGE_TYPE> keys = new List<DAMAGE_TYPE>();
    [SerializeField]
    List<int> values = new List<int>();

    public List<DAMAGE_TYPE> Keys { get => keys; set => keys = value; }
    public List<int> Values { get => values; set => values = value; }

    public int GetValueDamage(DAMAGE_TYPE key)
    {
        return Values[(int)key];
    }

    public void SetValueDamage(DAMAGE_TYPE key, int value)
    {
        Values[(int)key] = value;
    }

    public void AddValueDamage(DAMAGE_TYPE key, int value)
    {
        Values[(int)key] += value;
    }
}

[SerializeField]
public class WeaponSpriteType
{
    [SerializeField]
    List<WEAPON_TYPE> keys = new List<WEAPON_TYPE>();
    [SerializeField]
    List<int> values = new List<int>();

    public List<WEAPON_TYPE> Keys { get => keys; set => keys = value; }
    public List<int> Values { get => values; set => values = value; }
}