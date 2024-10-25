using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PanelEquipItems : MonoBehaviour
{
    [SerializeField] GameObject[] EquipItems;
    [SerializeField] InventoryEquipmentSO EquipmentSO;

    private void Start()
    {
        UIEquipment();
    }

    void UIEquipment()
    {
        for (int i = 0; i < EquipmentSO.weapon.Length; i++)
        {
            if (EquipmentSO.weapon[i] == null) return;
            if (EquipmentSO.weapon[i] != null)
            {
                for(int j = 0;EquipItems.Length > j; j++)
                {
                    if(EquipmentSO.weapon[i].Name == EquipItems[j].name)
                    {
                        EquipItems[j].GetComponent<Image>().sprite = EquipmentSO.weapon[i].ItemImage;
                        break;
                    }
                }
            }
        }
    }

    public void SetData(Sprite sprite, string name)
    {
        for (int i = 0; i < EquipItems.Length; i++)
        {
            if (EquipItems[i].name == name)
            {
                EquipItems[i].GetComponent<Image>().sprite = sprite;
                break;
            }
        }
    }
}
