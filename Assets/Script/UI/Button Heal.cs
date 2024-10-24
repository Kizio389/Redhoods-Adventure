﻿using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHeal : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] TextMeshProUGUI _button_text;
    [SerializeField] PlayerController _playerController;

    [SerializeField] InventorySO inventorySO;

    private int totalHpBottleCount = 0;
    private int totalMpBottleCount = 0;
    DataPlayer dataPlayer;
    private void Awake()
    {
        dataPlayer = DataPlayer.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetButtonHeal();
    }

    // Update is called once per frame
    void Update()
    {
        if (_button.name == "Button HP")
        {
            _button_text.text = totalHpBottleCount.ToString();
        }
        else if (_button.name == "Button MP")
        {
            _button_text.text = totalMpBottleCount.ToString();
        }
        //CheckButton();
    }

    public void SetButtonHeal()
    {
        totalHpBottleCount = 0;
        totalMpBottleCount = 0;

        foreach (var item in inventorySO.inventoryItems)
        {
            if (item.item == null) return;
            if (item.item.Name == "HP")
            {
                totalHpBottleCount += item.quantity;
            }
            else if (item.item.Name == "MP")
            {
                totalMpBottleCount += item.quantity;
            }
        }
    }

    void CheckButton()
    {
        if(_button.name == "Button HP" && (dataPlayer.Health >= dataPlayer.Max_Health || totalHpBottleCount == 0))
        {
            dataPlayer.Health = dataPlayer.Max_Health;
            _button.interactable = false;
        }
        else if(_button.name == "Button MP" && (dataPlayer.Energy >= dataPlayer.Max_Energy || totalMpBottleCount == 0))
        {
            dataPlayer.Energy = dataPlayer.Max_Energy;
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }
    }

    //public void ClickButtonHeal()
    //{
    //    if (_button.name == "Button HP" && totalHpBottleCount > 0)
    //    {
    //        totalHpBottleCount--;
    //        _button_text.text = totalHpBottleCount.ToString();
    //        _playerController.Healing(10.0f, _button.name);
    //        inventorySO.UseItem("HP", 1); // Giảm số lượng của item "Bottle HP" đi 1
    //    }
    //    else if (_button.name == "Button MP" && totalMpBottleCount > 0)
    //    {
    //        totalMpBottleCount--;
    //        _button_text.text = totalMpBottleCount.ToString();
    //        _playerController.Healing(10.0f, _button.name);
    //        inventorySO.UseItem("MP", 1); // Giảm số lượng của item "Bottle MP" đi 1
    //    }
    //}
}
