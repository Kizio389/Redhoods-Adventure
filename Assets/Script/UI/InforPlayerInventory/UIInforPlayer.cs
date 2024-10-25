using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInforPlayer : MonoBehaviour
{
    [SerializeField] private PlayerConfig Default_PlayerConfig;
    [SerializeField] private PlayerConfig Current_PlayerConfig;

    [field: SerializeField]
    private TextMeshProUGUI LevelTxt, PointSkillTxt, HPTxt, MPTxt, DamageTxt, ApTxt, ArmorTxt;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        SetText();
    }

    // Update is called once per frame
    void Update()
    {
        SetText();
    }

    private void SetText()
    {
        LevelTxt.text = "Level: " + Default_PlayerConfig.Level;
        PointSkillTxt.text = "Point Skill: " + Default_PlayerConfig.PointSkill;
        HPTxt.text = "HP: " + Current_PlayerConfig.MaxHp + "/" + Default_PlayerConfig.MaxHp;
        MPTxt.text = "MP: " + Current_PlayerConfig.MaxMp + "/" + Default_PlayerConfig.MaxMp;
        DamageTxt.text = "Damage: " + Default_PlayerConfig.DamgeAd;
        ApTxt.text = "Ap: " + Default_PlayerConfig.DamgeAp;
        ArmorTxt.text = "Armor: " + Default_PlayerConfig.Armor;
    }
}
