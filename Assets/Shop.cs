using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] ShopController shopController;
    // Start is called before the first frame update
    void Start()
    {
        shopController = FindObjectOfType<ShopController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shopController.LockShop(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        shopController.LockShop(false);
    }
}
