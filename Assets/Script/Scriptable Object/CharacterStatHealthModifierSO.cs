using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]

public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, string name, float val)
    {
        PlayerController player = character.GetComponent<PlayerController>();
        if(player != null)
        {
            Debug.Log("AffectCharacter");
            player.Healing(val, name);
        }
    }
}
