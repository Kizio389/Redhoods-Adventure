using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UIInventoryContent : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] List<GameObject> itemList = new List<GameObject>();
    [SerializeField] GameObject itemContent;

    [SerializeField] GameObject Panel_Description;
    [SerializeField] Image ItemImage_Description;

    [SerializeField] TextMeshProUGUI NameItem_Description;
    [SerializeField] TextMeshProUGUI DescriptionItem;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Sprite sprite, string nameItem, string description, bool Stack_able)
    {
        if(itemList.Count >= 25)
        {
            Debug.Log("Inventory Is Full");
            return;
        }
        else
        {
            //Add first item

            if (itemList.Count == 0)
            {
                InstantiateItemContent(nameItem, sprite, description);
            }
            else
            {
                GameObject _item = CheckList(nameItem, Stack_able);
                if (_item != null)
                {
                    _item.GetComponent<ItemInfor>().amount++;
                }
                else
                {
                    InstantiateItemContent(nameItem, sprite, description);
                }
            }
        }
    }

    GameObject CheckList(string nameItem, bool Stack_able)
    {
        foreach(var item in itemList)
        {
            if(item.GetComponent<ItemInfor>().ID_item == nameItem
                && item.GetComponent<ItemInfor>().stack_Able == Stack_able
                    && item.GetComponent<ItemInfor>().amount < 25)
            {
                return item;
            }
        }
        return null;
    }

    public void ShowDescription(ItemInfor item)
    {
        Panel_Description.SetActive(true);
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Chuyển đổi vị trí chuột sang tọa độ thế giới
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint
            (new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
        Panel_Description.transform.position = mouseWorldPosition;
        ItemImage_Description.sprite = item.item.GetComponent<Image>().sprite;
        NameItem_Description.text = item.ID_item + " x" + item.amount.ToString();
        DescriptionItem.text = item.Description;
    }
    public void HideDescription()
    {
        ItemImage_Description.sprite = null;
        Panel_Description.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HideDescription();
    }


    void InstantiateItemContent(string nameItem, Sprite sprite, string description)
    {
        GameObject newItem = Instantiate(itemContent, gameObject.transform);
        itemList.Add(newItem);
        newItem.GetComponent<ItemInfor>().ID_item = nameItem;
        newItem.GetComponent<ItemInfor>().item.SetActive(true);
        newItem.GetComponent<ItemInfor>().amount_text.gameObject.SetActive(true);
        newItem.GetComponent<ItemInfor>().item.GetComponent<Image>().sprite = sprite;
        newItem.GetComponent<ItemInfor>().empty = false;
        newItem.GetComponent<ItemInfor>().Description = description;
    }
}
