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
}
