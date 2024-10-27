using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class NewInventoryItem : MonoBehaviour,
    IPointerClickHandler
{
    public event Action<NewInventoryItem> OnItem, OutItem;
    UIInventoryContent inventoryContent;

    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryContent = GetComponentInParent<UIInventoryContent>();
        inventoryContent.ShowDescription(gameObject.GetComponent<ItemInfor>());
    }
}
