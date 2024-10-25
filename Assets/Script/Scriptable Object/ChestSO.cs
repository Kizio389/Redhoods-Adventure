using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChestSO : ScriptableObject
{
    public bool isOpenChest = false;
    [SerializeField] public GameObject Item;

    public void ResetChest()
    {
        isOpenChest = false;
    }
}
