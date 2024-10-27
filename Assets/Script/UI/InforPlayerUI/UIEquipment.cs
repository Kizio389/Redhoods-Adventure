using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipment : MonoBehaviour
{
    [SerializeField] List<GameObject> equipment = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(string name, Sprite sprite)
    {
        foreach (var item in equipment)
        {
            if (item.name == name)
            {
                item.GetComponentInChildren<Image>().sprite = sprite;
                return;
            }
        }
    }
}
