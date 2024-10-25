using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InforPlayer : MonoBehaviour
{
    [SerializeField] PlayerConfig default_PlayerConfig;
    [SerializeField] PlayerConfig current_PlayerConfig;

    [SerializeField] TextMeshProUGUI txt_HP;
    [SerializeField] TextMeshProUGUI txt_MP;
    [SerializeField] TextMeshProUGUI txt_Level;

    [SerializeField] Image imageEXP;


    private void Update()
    {
        UpdateUI();
    }
    void UpdateUI()
    {
        txt_HP.text = current_PlayerConfig.MaxHp.ToString() + "/" + default_PlayerConfig.MaxHp;
        txt_MP.text = current_PlayerConfig.MaxMp.ToString() + "/" + default_PlayerConfig.MaxMp;
        txt_Level.text = default_PlayerConfig.Level.ToString();
        if(current_PlayerConfig.Exp > 0)
        {
            Debug.Log("Fill");
            imageEXP.fillAmount = (float)current_PlayerConfig.Exp / default_PlayerConfig.MaxExp;
        }
        else if(current_PlayerConfig.Exp <= 0)
        {
            Debug.Log("Not fill");
            imageEXP.fillAmount = 0;
        }
    }
}
