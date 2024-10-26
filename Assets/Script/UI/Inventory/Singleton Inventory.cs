using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonInventory : MonoBehaviour
{
    public List<GameObject> Item = new List<GameObject>();

    private static SingletonInventory Inventory;

    public static SingletonInventory Instance
    {
        get
        {
            if(Inventory == null)
            {
                Inventory = new SingletonInventory();
            }
            return Inventory;
        }
    }
}
