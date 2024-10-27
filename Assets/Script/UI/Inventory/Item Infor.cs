using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemInfor : MonoBehaviour
{
    public string ID_item = "empty";
    public bool empty = true;
    public GameObject item;
    public int amount = 1;
    public TextMeshProUGUI amount_text;
    public bool stack_Able = true;
    public string Description;

    [Header("Type Restore")]
    public float amountRestoreHP;
    public float amountRestoreMP;
    [Header("Equipment")]
    public int Armor;
    public float MaxHealth;
    public float MaxEnergy;
    public float ADDamege;
    public float APDamege;
    public int ConditionLevel;
    private void Update()
    {
        amount_text.text = amount.ToString();
    }
}
