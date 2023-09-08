using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> _items;
    [SerializeField] private List<ItemInShopConfig> _itemConfig;
    //[SerializeField] private GameObject _cardItem;

    private void Start()
    {
    }

    public void SpawnCard(GameObject cardItem)
    {
        GameObject card = Instantiate(cardItem,transform.position,Quaternion.identity,transform) as GameObject;
        card.GetComponent<CardItem>().Init(GetRandomItem());
        _items.Add(card);
    }

    public void SpawnWeaponCard(GameObject cardWeapon, ItemInShopConfig itemConfig)
    {
        GameObject card = Instantiate(cardWeapon, transform.position, Quaternion.identity, transform) as GameObject;
        card.GetComponent<CardItem>().Init(itemConfig);
        _items.Add(card);
    }

    public ItemInShopConfig GetRandomItem()
    {
        ItemInShopConfig cardConfig = _itemConfig[Random.Range(0, _itemConfig.Count)];
        return cardConfig;
    }
}
