using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIIndexController : MonoBehaviour
{
    [SerializeField] List<IndexPlayerController> indexPlayer = new List<IndexPlayerController>();

    [SerializeField] string isObject;
    [SerializeField] TextMeshProUGUI skillPointText;
    [SerializeField] GameObject SkillPoint;
    [SerializeField] Button BtnPlus;
    [SerializeField] Button BtnMinus;
    DataPlayer dataPlayer;
    private void Awake()
    {
        dataPlayer = DataPlayer.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {

        SkillPoint.SetActive(false);
        foreach (var item in indexPlayer)
        {
            item.OnIndexClick += ClickOnIndex;
            //Debug.Log("Đã đăng ký sự kiện cho: " + item.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        skillPointText.text = dataPlayer.SkillPoint.ToString();
    }

    void ClickOnIndex(IndexPlayerController item)
    {
        ResetColor();
        //Debug.Log("Đã click vào item: " + item.gameObject.name);
        int index = indexPlayer.IndexOf(item);
        if (index == -1) 
        { 
            ResetColor();
            return;
        }
        isObject= item.gameObject.name;
        Color colorText = item.gameObject.GetComponentInChildren<TextMeshProUGUI>().color;
        if (item.gameObject.name == "Index HP") colorText = Color.red;
        else if (item.gameObject.name == "Index MP") colorText = Color.blue;
        else if (item.gameObject.name == "Index AD Damge") colorText = Color.white;
        else if (item.gameObject.name == "Index AP Damge") colorText = Color.cyan;
        else if (item.gameObject.name == "Index Armor") colorText = Color.gray;
        item.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = colorText;
        ShowPanelSkillPoint();
    }

    void ResetColor()
    {
        foreach (var item in indexPlayer)
        {
            item.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
    }
    void ShowPanelSkillPoint()
    {
        SkillPoint.SetActive(true);
    }

    public void PlusSkill()
    {
        if(dataPlayer.SkillPoint<=0) return;
        switch(isObject)
        {
            case "Index HP":
                dataPlayer.Max_Health += 1;
                break;
            case "Index MP":
                dataPlayer.Max_Energy += 1;
                break;
            case "Index AD Damge":
                dataPlayer.AD_Damage += 1;
                break;
            case "Index AP Damge":
                dataPlayer.AP_Damage += 1;
                break;
            case "Index Armor":
                dataPlayer.Armor += 1;
                break;
                default:
                break;
        }
        dataPlayer.SkillPoint--;
    }
}
