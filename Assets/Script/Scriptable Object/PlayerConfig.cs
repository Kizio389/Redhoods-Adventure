using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "GameConfiguration/Player", order = 1)]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public int Level { get; set; }
    [field: SerializeField] public int Exp { get; set; }
    [field: SerializeField] public int MaxExp { get; set; }
    [field: SerializeField] public int PointSkill { get; set; }

    [field: SerializeField] public float DamgeAd { get; set; }
    [field: SerializeField] public float DamgeAp { get; set; }

    [field: SerializeField] public float MaxHp { get; set; }
    [field: SerializeField] public float MaxMp { get; set; }
    [field: SerializeField] public float Armor { get; set; }

    [field: SerializeField] public int Coin { get; set; }
    [field: SerializeField] public int Gem { get; set; }

    public void ResetToDefault()
    {
        Level = 0;
        Exp = 0;
        MaxExp = 100;
        PointSkill = 0;
        DamgeAd = 10;
        DamgeAp = 10;
        MaxHp = 100;
        MaxMp = 100;
        Armor = 0;
        Coin = 0;
        Gem = 0;
    }
}
