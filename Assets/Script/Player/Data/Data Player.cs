using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayer
{
    //Singleton
    private static DataPlayer Data;
    public float Max_Health { get; set; }
    public float Health { get; set; }
    public float Max_Energy { get; set; }
    public float Energy { get; set; }
    public int Coin { get; set; }
    public int Gem { get; set; }
    public int Armor { get; set; }
    public float AD_Damage { get; set; }
    public float AP_Damage { get; set; }
    public int Level { get; set; }
    public int Max_EXP { get; set; }
    public int EXP { get; set; }
    public int SkillPoint { get; set; }

    private DataPlayer()
    {
        Max_Health = 100f;
        Health = 100f;
        Max_Energy = 50f;
        Energy = 50f;
        Coin = 0;
        Gem = 0;
        Armor = 0;
        AD_Damage = 10.0f;
        AP_Damage = 5.0f;
        Level = 1;
        Max_EXP = 300;
        EXP = 0;
        SkillPoint = 0;
    }

    public static DataPlayer Instance
    {
        get
        {
            if (Data == null)
            {
                Data = new DataPlayer();
            }
            return Data;
        }
    }

    public void ResetData()
    {
        Max_Health = 100f;
        Health = Max_Health;
        Max_Energy = 50f;
        Energy = Max_Energy;
        Coin = 0;
        Gem = 0;
        Armor = 0;
        AD_Damage = 10.0f;
        AP_Damage = 5.0f;
        Level = 1;
        Max_EXP = 300;
        EXP = 0;
        SkillPoint = 0;
    }
}
