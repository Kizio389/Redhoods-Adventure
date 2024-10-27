using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [Header("UI Item")]
    public Sprite imageItem;
    public string NameItem;
    public bool stack_able;

    [Header("Type Restore")]
    public float amountRestoreHP;
    public float amountRestoreMP;
    [Header("Equipment")]
    public int Armor;
    public float MaxHealth;
    public float MaxEnergy;
    public float ADDamege;
    public float APDamege;
    public int ConditionLevel;

    [TextArea (0,10)]
    public string Description;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float duration = .3f;

    private void Start()
    {
       imageItem = GetComponent<SpriteRenderer>().sprite;
       NameItem = gameObject.name;
    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickup());
    }

    private IEnumerator AnimateItemPickup()
    {
        audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        transform.localScale = endScale;
        Destroy(gameObject);
    }
}
