using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Inventory.Model;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    [SerializeField] private UIShopPage shopUI;

    [SerializeField] private InventorySO inventorySO;

    [SerializeField] private GameObject NotificationBuy;
    [SerializeField] private TextMeshProUGUI NotificationTxt;

    [SerializeField] private PlayerConfig playerConfig;

    [SerializeField] private ShopSO shopData;
    bool Locked;

    public List<ShopItem> initialItems = new List<ShopItem>();

    private void Awake()
    {
        NotificationTxt = NotificationBuy.GetComponent<TextMeshProUGUI>();

        //NotificationBuy.SetActive(false);
    }

    private void Start()
    {
        PrepareUI();
        PrepareShopData();
    }
    private void PrepareShopData()
    {
        //shopData.Initialize();
        shopData.OnShopUpdated += UpdateShopUI;
        foreach (ShopItem item in initialItems)
        {
            if (item.IsEmpty) continue;
            shopData.AddItem(item);
        }
    }

    private void UpdateShopUI(Dictionary<int, ShopItem> ShopState)
    {
        shopUI.ResetAllItem();
        foreach (var item in ShopState)
        {
            shopUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
        }
    }

    private void PrepareUI()
    {
        shopUI.InitializeShopUI(shopData.Size);
        shopUI.OnDescriptionRequested += HandleDescriptionRequest;
        shopUI.OnItemActionRequested += HandleItemActionRequest;
    }

    private void HandleItemActionRequest(int ItemIndex)
    {
        ShopItem ShopItem = shopData.GetItemAt(ItemIndex);
        if (ShopItem.IsEmpty) return;

        IItemAction itemAction = ShopItem.item as IItemAction;
        if (itemAction != null)
        {
            if(playerConfig.Coin < ShopItem.item.Price)
            {
                StartCoroutine(ShowPanelNotification("Can't buy!"));
                return;
            }
            else 
            {
                inventorySO.AddItem(ShopItem.item, 1);
                playerConfig.Coin -= ShopItem.item.Price;
                GetComponent<PlayerController>().SetCoin();
                StartCoroutine(ShowPanelNotification("Buy complete"));
            }
            
        }
    }

    IEnumerator ShowPanelNotification(string text)
    {
        NotificationBuy.SetActive(true);
        NotificationTxt.text = text;
        yield return new WaitForSeconds(.5f);
        NotificationBuy.SetActive(false);
    }

    private void HandleDescriptionRequest(int ItemIndex)
    {
        ShopItem ShopItem = shopData.GetItemAt(ItemIndex);
        if (ShopItem.IsEmpty)
        {
            shopUI.ResetSelection();
            return;
        }
        ItemSO item = ShopItem.item;
        string description = PrepareDescription(ShopItem);
        shopUI.UpdateDescription(ItemIndex, item.ItemImage,
            item.name, description);
    }

    public string PrepareDescription(ShopItem ShopItem)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(ShopItem.item.Description);
        sb.AppendLine();
        for (int i = 0; i < ShopItem.itemState.Count; i++)
        {
            sb.Append("Price" +
                $":{ShopItem.itemState[i].Price}");
            sb.AppendLine();
        }
        return sb.ToString();
    }
    public bool LockShop(bool _Lock)
    {
        return Locked = _Lock;
    }
    public void Update()
    {
        Debug.Log(Locked);
        if (Locked == false) return;
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (shopUI.isActiveAndEnabled == false)
                {
                    shopUI.Show();
                    foreach (var item in shopData.GetCurrentShopState())
                    {
                        shopUI.UpdateData(item.Key,
                            item.Value.item.ItemImage,
                            item.Value.quantity);
                    }
                }
                else
                {
                    shopUI.Hide();
                }
            }
        }
        
    }
}
