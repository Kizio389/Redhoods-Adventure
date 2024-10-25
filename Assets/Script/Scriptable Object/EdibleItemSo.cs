using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EdibleItemSo : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField]
        private List<ModifierData> modifiersData = new List<ModifierData>();
        public string ActionName => "Consume";

        public AudioClip actionSFX { get;private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            Debug.Log("PerformAction");
            foreach (ModifierData data in modifiersData)
            {
                data.statModifier.AffectCharacter(character,Name, data.Value);
            }
            return true;
        }
    }

    public interface IDestroyableItem
    {

    }

    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip actionSFX { get; }
        bool PerformAction(GameObject character, List<ItemParameter> itemState);
    }
    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        public float Value;
    }
}