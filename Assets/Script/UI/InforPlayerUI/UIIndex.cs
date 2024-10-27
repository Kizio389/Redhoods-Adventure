using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIIndex : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI HP, MP, AD, AP, Armor, Level;

    DataPlayer dataPlayer;
    // Start is called before the first frame update
    void Start()
    {
        dataPlayer = DataPlayer.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        HP.text = "HP: " + dataPlayer.Max_Health.ToString();
        MP.text = "MP: " + dataPlayer.Max_Energy.ToString();
        AD.text = "AD Damege:\n" + dataPlayer.AD_Damage.ToString();
        AP.text = "AP Damege:\n" + dataPlayer.AP_Damage.ToString();
        Armor.text = "Armor: " + dataPlayer.Armor.ToString();
        Level.text = "Level " + dataPlayer.Level.ToString();
    }
}
