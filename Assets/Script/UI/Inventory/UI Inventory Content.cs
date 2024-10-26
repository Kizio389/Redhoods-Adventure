using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UIInventoryContent : MonoBehaviour
{
    [SerializeField] List<GameObject> itemList = new List<GameObject>();
    [SerializeField] GameObject itemContent;
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
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddItem(gameObject.GetComponent<Sprite>(), "HP");
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            AddItem(gameObject.GetComponent<Sprite>(), "MP");
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            AddItem(gameObject.GetComponent<Sprite>(), "Name");
        }
    }

    public void AddItem(Sprite sprite, string nameItem)
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
                Debug.Log("add fst Item");
                GameObject newItem = Instantiate(itemContent, gameObject.transform);
                itemList.Add(newItem);
                newItem.GetComponent<ItemInfor>().ID_item = nameItem;
                newItem.GetComponent<ItemInfor>().item.SetActive(true);
                newItem.GetComponent<ItemInfor>().amount_text.gameObject.SetActive(true);
                newItem.GetComponent<ItemInfor>().item.GetComponent<Image>().sprite = sprite;
                newItem.GetComponent<ItemInfor>().empty = false;
            }
            else
            {
                if (CheckList(nameItem) != null)
                {
                    CheckList(nameItem).GetComponent<ItemInfor>().amount++;
                }
                else
                {
                    GameObject newItem = Instantiate(itemContent, gameObject.transform);
                    itemList.Add(newItem);
                    newItem.GetComponent<ItemInfor>().ID_item = nameItem;
                    newItem.GetComponent<ItemInfor>().item.SetActive(true);
                    newItem.GetComponent<ItemInfor>().amount_text.gameObject.SetActive(true);
                    newItem.GetComponent<ItemInfor>().item.GetComponent<Image>().sprite = sprite;
                    newItem.GetComponent<ItemInfor>().empty = false;
                }
            }
        }
    }

    GameObject CheckList(string nameItem)
    {
        foreach(var item in itemList)
        {
            if(item.GetComponent<ItemInfor>().ID_item == nameItem)
            {
                return item;
            }
        }
        return null;
    }
}
