using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonIndex : MonoBehaviour, IPointerClickHandler
{
    public event Action<ButtonIndex> OnLeftMouseBtnClick, OnItemClicked;

    IndexPlayer indexPlayer;

    private void Start()
    {
        indexPlayer = FindObjectOfType<IndexPlayer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            indexPlayer.SetNameButton(gameObject.name);
            Debug.Log(gameObject.name);
            OnLeftMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
