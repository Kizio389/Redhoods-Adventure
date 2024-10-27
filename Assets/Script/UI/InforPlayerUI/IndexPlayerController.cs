using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class IndexPlayerController : MonoBehaviour, IPointerClickHandler
{
    public event Action<IndexPlayerController> OnIndexClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("Chuột trái đã click vào: " + gameObject.name);
            OnIndexClick?.Invoke(this);
        }
    }
}
