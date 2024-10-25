using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UIInventoryPage inventoryUI;

        [SerializeField] private IndexPlayer IndexPlayerPanel;

        [SerializeField] private InventorySO inventoryData;

        [SerializeField] private PlayerConfig currentPlayerConfig;
        [SerializeField] private PlayerConfig defaultPlayerConfig;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }
        private void PrepareInventoryData()
        {
            //inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if(item.IsEmpty) continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> InventoryState)
        {
            inventoryUI.ResetAllItem();
            foreach (var item in InventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int ItemIndex)
        {
            Debug.Log("Inventory Controller");
            InventoryItem inventoryItem = inventoryData.GetItemAt(ItemIndex);
            if (inventoryItem.IsEmpty)
                return;
            if(inventoryItem.item.EdibleItem == true)
            {
                if (inventoryItem.item.Name == "HP")
                {
                    if (currentPlayerConfig.MaxHp >= defaultPlayerConfig.MaxHp) return;
                    IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
                    if (destroyableItem != null)
                    {
                        Debug.Log("IDestroyableItem");
                        inventoryData.RemoveItem(ItemIndex, 1);
                    }

                    IItemAction itemAction = inventoryItem.item as IItemAction;
                    if (itemAction != null)
                    {
                        Debug.Log("IItemAction");
                        itemAction.PerformAction(gameObject, inventoryItem.itemState);
                        if (inventoryData.GetItemAt(ItemIndex).IsEmpty)
                            inventoryUI.ResetSelection();
                    }
                }

                else if (inventoryItem.item.Name == "MP")
                {
                    if (currentPlayerConfig.MaxMp >= defaultPlayerConfig.MaxMp) return;
                    IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
                    if (destroyableItem != null)
                    {
                        Debug.Log("IDestroyableItem");
                        inventoryData.RemoveItem(ItemIndex, 1);
                    }

                    IItemAction itemAction = inventoryItem.item as IItemAction;
                    if (itemAction != null)
                    {
                        Debug.Log("IItemAction");
                        itemAction.PerformAction(gameObject, inventoryItem.itemState);
                        if (inventoryData.GetItemAt(ItemIndex).IsEmpty)
                            inventoryUI.ResetSelection();
                    }
                }
            }
            else if(inventoryItem.item.EquipItem == true)
            {
                IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
                if (destroyableItem != null)
                {
                    Debug.Log("IDestroyableItem");
                    inventoryData.RemoveItem(ItemIndex, 1);
                }

                IItemAction itemAction = inventoryItem.item as IItemAction;
                if (itemAction != null)
                {
                    Debug.Log("IItemAction");
                    itemAction.PerformAction(gameObject, inventoryItem.itemState);
                    if (inventoryData.GetItemAt(ItemIndex).IsEmpty)
                        inventoryUI.ResetSelection();
                }
            }
        }

        private void HandleDragging(int ItemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(ItemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int ItemIndex_1, int ItemIndex_2)
        {
            inventoryData.SwapItems(ItemIndex_1, ItemIndex_2);
        }

        private void HandleDescriptionRequest(int ItemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(ItemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(ItemIndex, item.ItemImage,
                item.name, description);
        }

        public string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName}" +
                    $"{inventoryItem.itemState[i].value}");
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key,
                            item.Value.item.ItemImage,
                            item.Value.quantity);
                    }
                }
                else
                {
                    inventoryUI.Hide();
                    IndexPlayerPanel.ClosePanelIndexPlayer();
                }
            }
        }
    }
}