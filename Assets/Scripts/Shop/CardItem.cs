using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardItem : MonoBehaviour
{
    [SerializeField] private ItemInShopConfig _shopConfig;
    [SerializeField] private Image _bg;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _content;

    public void Init(ItemInShopConfig shopConfig)
    {
        _shopConfig = shopConfig;
        _icon.sprite = shopConfig.Sprite;
        RareItem(shopConfig.RareType);
        if(_content != null)
        _content.text = shopConfig.Description;
    }

    private void RareItem(RARE_TYPE rare)
    {
        switch (rare)
        {
            case RARE_TYPE.NORMAL:
                _bg.color = Color.white;
                break;
            case RARE_TYPE.BLUE:
                _bg.color = Color.blue;
                break;
            case RARE_TYPE.VIOLET:
                _bg.color = new Color(255,0,255);
                break;
            case RARE_TYPE.ORANGE:
                _bg.color = new Color(255, 167, 34);
                break;
            case RARE_TYPE.RED:
                _bg.color = Color.red;
                break;
        }
    }

    public void OnClick()
    {
        gameObject.SetActive(false);
        ShopController.Instance.BuyCard(this);
    }

    public ItemInShopConfig GetItemConfig()
    {
        return _shopConfig;
    }
}
