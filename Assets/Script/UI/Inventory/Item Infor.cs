using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemInfor :MonoBehaviour
{
    public string ID_item = "empty";
    public bool empty = true;
    public GameObject item;
    public int amount = 0;
    public TextMeshProUGUI amount_text;

    private void Update()
    {
        amount_text.text = amount.ToString();
    }
}
