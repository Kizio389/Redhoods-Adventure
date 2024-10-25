using System;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ShopSO : ScriptableObject
{
    [SerializeField] public List<ShopItem> ShopItems;

    [field: SerializeField]
    public int Size { get; private set; } = 10;

    public event Action<Dictionary<int, ShopItem>> OnShopUpdated;

    public void Initialize()
    {
        ShopItems = new List<ShopItem>();
        for (int i = 0; i < Size; i++)
        {
            ShopItems.Add(ShopItem.GetEmptyItem());
        }
    }

    public int AddItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)
    {
        if (item.IsStackable == false)
        {
            for (int i = 0; i < ShopItems.Count; i++)
            {
                InformAboutChange();
                return quantity;
            }
        }
        return quantity;
    }
    public Dictionary<int, ShopItem> GetCurrentShopState()
    {
        Dictionary<int, ShopItem> returnValue =
            new Dictionary<int, ShopItem>();

        for (int i = 0; i < ShopItems.Count; i++)
        {
            if (ShopItems[i].IsEmpty)
                continue;
            returnValue[i] = ShopItems[i];
        }
        return returnValue;
    }
    private void InformAboutChange()
    {
        OnShopUpdated?.Invoke(GetCurrentShopState());
    }
    public void AddItem(ShopItem item)
    {
        AddItem(item.item, item.quantity);
    }

    public ShopItem GetItemAt(int itemIndex)
    {
        return ShopItems[itemIndex];
    }
}

[Serializable]

public struct ShopItem
{
    public int quantity;
    public ItemSO item;
    public List<ItemParameter> itemState;
    public bool IsEmpty => item == null;

    public static ShopItem GetEmptyItem()
            => new ShopItem
            {
                item = null,
                itemState = new List<ItemParameter>(0)
            };
}

