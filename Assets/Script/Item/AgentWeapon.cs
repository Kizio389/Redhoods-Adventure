using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;

    [SerializeField] InventoryEquipmentSO EquipmentSO;
    [SerializeField] PanelEquipItems panelEquipItems;
    public void SetWeapon(EquippableItemSO weaponItemSo, List<ItemParameter> itemState)
    {
        for (int i = 0;i < EquipmentSO.weapon.Length;i++)
        {
            Debug.Log("SetWeapon");
            if (EquipmentSO.weapon[i] == null)
            {
                this.EquipmentSO.weapon[i] = weaponItemSo;
                this.itemCurrentState = new List<ItemParameter>(itemState);
                EquipmentSO.UpdateIndexEquipment(EquipmentSO.weapon[i].Name, itemState[0].value);
                panelEquipItems.SetData(EquipmentSO.weapon[i].ItemImage, EquipmentSO.weapon[i].Name);
                break;
            }
            else if (EquipmentSO.weapon[i] != null)
            {
                if (EquipmentSO.weapon[i].Name == weaponItemSo.Name)
                {
                    inventoryData.AddItem(EquipmentSO.weapon[i], 1, itemCurrentState);
                    this.EquipmentSO.weapon[i] = weaponItemSo;
                    this.itemCurrentState = new List<ItemParameter>(itemState);
                    EquipmentSO.UpdateIndexEquipment(EquipmentSO.weapon[i].Name, itemState[0].value);
                    panelEquipItems.SetData(EquipmentSO.weapon[i].ItemImage,EquipmentSO.weapon[i].Name);
                    break;
                }
            }
            
            //ModifyParameters();

        }

    }

    private void ModifyParameters()
    {
        Debug.Log("ModifyParameters");
        foreach (var parameter in parametersToModify)
        {
            if(itemCurrentState.Contains(parameter))
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue
                };
            }
        }
    }
}
