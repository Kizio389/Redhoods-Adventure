using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IndexPlayer : MonoBehaviour
{
    [SerializeField] GameObject panelPointSkill;

    [SerializeField] PlayerConfig playerConfig;

    string Name_Buttons;
    bool allowMinus;

    int Temp_PS_Hp, Temp_PS_Mp, Temp_PS_Damage, Temp_PS_AP;

    private void Start()
    {
        panelPointSkill.SetActive(false);
        Name_Buttons = null;
        allowMinus = false;
        Temp_PS_Hp = Temp_PS_Mp = Temp_PS_Damage = Temp_PS_AP = 0;
    }

    public void ClosePanelIndexPlayer()
    {
        panelPointSkill.SetActive(false);
        Name_Buttons = null;
        allowMinus = false;
        Temp_PS_Hp = Temp_PS_Mp = Temp_PS_Damage = Temp_PS_AP = 0;
    }

    public void SetNameButton(string Name_button)
    {
        panelPointSkill.SetActive(true);
        Name_Buttons = Name_button;
    }

    public void PlusIndex()
    {
        if(playerConfig.PointSkill <= 0 ) return;
        else 
        {
            if (Name_Buttons == "btn HP")
            {
                playerConfig.MaxHp += 10;
                playerConfig.PointSkill--;
                Temp_PS_Hp++;
                allowMinus = true;
            }
            else if (Name_Buttons == "btn MP")
            {
                playerConfig.MaxMp += 10;
                playerConfig.PointSkill--;
                Temp_PS_Mp++;
                allowMinus = true;
            }
            else if (Name_Buttons == "btn Damage")
            {
                playerConfig.DamgeAd += 10;
                playerConfig.PointSkill--;
                Temp_PS_Damage++;
                allowMinus = true;
            }
            else if (Name_Buttons == "btn AP")
            {
                playerConfig.DamgeAp += 10;
                playerConfig.PointSkill--;
                Temp_PS_AP++;
                allowMinus = true;
            }
        }
    }

    public void MinusIndex()
    {
        if (allowMinus == false) return;
        else
        {
            if (Name_Buttons == "btn HP" && Temp_PS_Hp > 0)
            {
                playerConfig.MaxHp -= 10;
                Temp_PS_Hp--;
                playerConfig.PointSkill++;
            }
            else if (Name_Buttons == "btn MP" && Temp_PS_Mp > 0)
            {
                playerConfig.MaxMp -= 10;
                Temp_PS_Mp--;
                playerConfig.PointSkill++;
            }
            else if (Name_Buttons == "btn Damage" && Temp_PS_Damage > 0)
            {
                playerConfig.DamgeAd -= 10;
                Temp_PS_Damage--;
                playerConfig.PointSkill++;
            }
            else if (Name_Buttons == "btn AP" && Temp_PS_AP > 0)
            {
                playerConfig.DamgeAp -= 10;
                Temp_PS_AP--;
                playerConfig.PointSkill++;
            }
        }
    }
}
