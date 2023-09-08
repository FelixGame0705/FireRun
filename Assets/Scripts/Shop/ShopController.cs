using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : Singleton<ShopController>
{
    [SerializeField] private CardSystem _cardSystem;
    [SerializeField] private CardSystem _cardWeaponSystem;
    [SerializeField] private GameObject _cardItemModel;
    [SerializeField] private GameObject _cardWeaponModel;
    [SerializeField] private List<CardItem> _cardBuy;
    [SerializeField] private List<CardItem> _weaponCards;
    [SerializeField] private List<CardItem> _itemCards;
    [SerializeField] private PlayerUpDownController _playerUpDown;

    private void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            GenerateItemCard();
        }
    }

    public void BuyCard(CardItem item)
    {
        _cardBuy.Add(item);
        ExecuteItem(item);
    }

    private PlayerConfig _playerConfig;
    public void ExecuteItem(CardItem item)
    {
        _playerConfig = _playerUpDown.GetPlayerConfig();
        _playerConfig.MaxHP += item.GetItemConfig().MaxHP;
        _playerConfig.HpGeneration += item.GetItemConfig().HpRegeneration;
        _playerConfig.LifeSteal += item.GetItemConfig().LifeSteal;
        _playerConfig.CritChance += item.GetItemConfig().CritChance;
        _playerConfig.RangedAttack += item.GetItemConfig().RangedAttack;
        _playerConfig.Speed += item.GetItemConfig().Speed;
        _playerConfig.Harvesting += item.GetItemConfig().Harvesting;
        _playerConfig.Armor += item.GetItemConfig().Armor;
        _playerConfig.AttackedSpeed += item.GetItemConfig().AttackedSpeed;
        DamageItem(item);
        EquipItemWeapon(item);
    }

    private void DamageItem(CardItem item)
    {
        int meleeDamage = item.GetItemConfig().DamageTypes.GetValueDamage(DAMAGE_TYPE.MELEE);
        _playerConfig.DamageTypes.AddValueDamage(DAMAGE_TYPE.MELEE, meleeDamage);
        int rangedDamage = item.GetItemConfig().DamageTypes.GetValueDamage(DAMAGE_TYPE.RANGED);
        _playerConfig.DamageTypes.AddValueDamage(DAMAGE_TYPE.RANGED, rangedDamage);
        int fireDamage = item.GetItemConfig().DamageTypes.GetValueDamage(DAMAGE_TYPE.FIRE);
        _playerConfig.DamageTypes.AddValueDamage(DAMAGE_TYPE.FIRE, fireDamage);
        int poisonDamage = item.GetItemConfig().DamageTypes.GetValueDamage(DAMAGE_TYPE.POISON);
        _playerConfig.DamageTypes.AddValueDamage(DAMAGE_TYPE.POISON, poisonDamage);
    }

    private void EquipItemWeapon(CardItem item)
    {
        if (item.GetItemConfig().ItemType == ITEM_TYPE.Weapon)
        {
            _weaponCards.Add(item);
            _cardWeaponSystem.SpawnWeaponCard(_cardWeaponModel, item.GetItemConfig());
        }
    }

    private void GenerateItemCard()
    {
        _cardSystem.SpawnCard(_cardItemModel);
    }

    public void OnClickContinue()
    {
        GamePlayController.Instance.UpdateStateGame(STATE_OF_GAME.PLAYING);
        ShopController.Instance.gameObject.SetActive(false);
    }
}
