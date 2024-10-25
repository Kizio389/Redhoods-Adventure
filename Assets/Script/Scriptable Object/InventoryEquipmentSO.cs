using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class InventoryEquipmentSO : ScriptableObject
{
    [SerializeField]
    public EquippableItemSO[] weapon;

    [SerializeField] PlayerConfig defaultPlayerConfig;
    [SerializeField] PlayerConfig currentPlayerConfig;

    private void Awake()
    {
        
    }

    public void ResetData()
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i] = null;
        }
    }

    public void UpdateIndexEquipment(string nameItem, float value)
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            if (weapon[i].Name == nameItem)
            {
                float tempArmor = defaultPlayerConfig.Armor - currentPlayerConfig.Armor;
                float tempDamage =  defaultPlayerConfig.DamgeAd - currentPlayerConfig.DamgeAd;
                float tempAP = defaultPlayerConfig.DamgeAp - currentPlayerConfig.DamgeAp;
                float tempMP = defaultPlayerConfig.MaxMp - currentPlayerConfig.MaxMp;
                float tempHP = defaultPlayerConfig.MaxHp - currentPlayerConfig.MaxHp;
                switch (nameItem)
                {

                    case "Armor":
                        defaultPlayerConfig.Armor -= tempArmor;
                        defaultPlayerConfig.Armor += value;
                        break;
                    case "Trouser":
                        defaultPlayerConfig.Armor -= tempArmor;
                        defaultPlayerConfig.Armor += value;
                        break;
                    case "Sword":
                        defaultPlayerConfig.DamgeAd -= tempDamage;
                        defaultPlayerConfig.DamgeAd += value;
                        break;
                    case "Ring":
                        defaultPlayerConfig.DamgeAp -= tempAP;
                        defaultPlayerConfig.DamgeAp += value;
                        break;
                    case "Helmet":
                        defaultPlayerConfig.MaxHp -= tempHP;
                        defaultPlayerConfig.MaxHp += value;
                        break;
                    case "Boots":
                        defaultPlayerConfig.MaxMp -= tempMP;
                        defaultPlayerConfig.MaxMp += value;
                        break;
                    default:
                        break;
                }
                break;
            }
        }
    }
}
