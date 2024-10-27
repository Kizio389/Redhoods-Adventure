using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InforPlayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt_HP;
    [SerializeField] TextMeshProUGUI txt_MP;
    [SerializeField] TextMeshProUGUI txt_Level;

    [SerializeField] Image imageEXP;

    DataPlayer dataPlayer;

    private void Awake()
    {
        dataPlayer = DataPlayer.Instance;
    }

    private void Update()
    {
        UpdateUI();
    }
    void UpdateUI()
    {
        txt_HP.text = dataPlayer.Health.ToString() + "/" + dataPlayer.Max_Health;
        txt_MP.text = dataPlayer.Energy.ToString() + "/" + dataPlayer.Max_Energy;
        txt_Level.text = dataPlayer.Level.ToString();
        if(dataPlayer.EXP > 0)
        {
            //Debug.Log("Fill");
            imageEXP.fillAmount = (float)dataPlayer.EXP / dataPlayer.Max_EXP;
        }
        else if(dataPlayer.EXP <= 0)
        {
            //Debug.Log("Not fill");
            imageEXP.fillAmount = 0;
        }
    }
}
