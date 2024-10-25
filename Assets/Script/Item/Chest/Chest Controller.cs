using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    Animator _Animator;
    // Start is called before the first frame update

    [SerializeField] ChestSO ChestSO;
    void Start()
    {
        _Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _Animator.SetBool("Open_Chest", ChestSO.isOpenChest);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(ChestSO.isOpenChest == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                ChestSO.isOpenChest = true;
                _Animator.SetBool("Open_Chest", ChestSO.isOpenChest);
                Instantiate(ChestSO.Item, transform.position, Quaternion.identity);
            }
        }   
        
    }
}
