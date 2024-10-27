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
    public GameObject isObject;
     
    [SerializeField] GameObject Panel_Description;
    [SerializeField] Image ItemImage_Description;

    [SerializeField] TextMeshProUGUI NameItem_Description;
    [SerializeField] TextMeshProUGUI DescriptionItem;

    [SerializeField] UIEquipment Equipment;

    [SerializeField] Button Btn_Use;
    [SerializeField] Button Btn_Equip;

    DataPlayer dataPlayer;
    private void Awake()
    {
        dataPlayer = DataPlayer.Instance;
        Panel_Description.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item item)
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
                InstantiateItemContent(item);
            }
            else
            {
                GameObject _item = CheckList(item);
                if (_item != null)
                {
                    _item.GetComponent<ItemInfor>().amount++;
                }
                else
                {
                    InstantiateItemContent(item);
                }
            }
        }
    }

    GameObject CheckList(Item _item)
    {
        foreach(var item in itemList)
        {
            if (item.GetComponent<ItemInfor>().ID_item == _item.NameItem
                && _item.stack_able == true
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
        if(item.stack_Able == true)
        {
            Btn_Use.gameObject.SetActive(true);
            Btn_Equip.gameObject.SetActive(false);
        }
        else
        {
            Btn_Use.gameObject.SetActive(false);
            Btn_Equip.gameObject.SetActive(true);
        }
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


    void InstantiateItemContent(Item item)
    {
        GameObject newItem = Instantiate(itemContent, gameObject.transform);
        itemList.Add(newItem);
        newItem.GetComponent<ItemInfor>().ID_item = item.NameItem;
        newItem.GetComponent<ItemInfor>().item.SetActive(true);
        newItem.GetComponent<ItemInfor>().amount_text.gameObject.SetActive(true);
        newItem.GetComponent<ItemInfor>().item.GetComponent<Image>().sprite = item.imageItem;
        newItem.GetComponent<ItemInfor>().empty = false;
        newItem.GetComponent<ItemInfor>().Description = item.Description;
        newItem.GetComponent<ItemInfor>().stack_Able = item.stack_able;
        newItem.GetComponent<ItemInfor>().amountRestoreHP = item.amountRestoreHP;
        newItem.GetComponent<ItemInfor>().amountRestoreMP = item.amountRestoreMP;
        newItem.GetComponent<ItemInfor>().Armor = item.Armor;
        newItem.GetComponent<ItemInfor>().MaxHealth = item.MaxHealth;
        newItem.GetComponent<ItemInfor>().MaxEnergy = item.MaxEnergy;
        newItem.GetComponent<ItemInfor>().ADDamege = item.ADDamege;
        newItem.GetComponent<ItemInfor>().APDamege = item.APDamege;
        newItem.GetComponent<ItemInfor>().ConditionLevel = item.ConditionLevel;
    }

    public void UseItem()
    {

    }

    public void EquipItem()
    {
        foreach (var item in itemList)
        {
            if(item.GetComponent<ItemInfor>().ID_item == isObject.GetComponent<ItemInfor>().ID_item)
            {
                
                if(dataPlayer.Level == item.GetComponent<ItemInfor>().ConditionLevel)
                {
                    Equipment.AddItem(item.GetComponent<ItemInfor>().ID_item,
                    item.GetComponent<ItemInfor>().item.GetComponent<Image>().sprite);
                    PlusIndex(item.GetComponent<ItemInfor>().MaxHealth,
                        item.GetComponent<ItemInfor>().MaxEnergy,
                        item.GetComponent<ItemInfor>().Armor,
                        item.GetComponent<ItemInfor>().ADDamege,
                        item.GetComponent<ItemInfor>().APDamege);
                    itemList.Remove(item);
                    Destroy(item.gameObject);
                }
                HideDescription();
                return;
            }
            
        }
    }

    void PlusIndex(float HP, float MP, int Armor, float AD, float AP)
    {
        dataPlayer.Max_Health += HP;
        dataPlayer.Max_Energy += MP;
        dataPlayer.Armor += Armor;
        dataPlayer.AD_Damage += AD;
        dataPlayer.AP_Damage += AP;
    }
}
