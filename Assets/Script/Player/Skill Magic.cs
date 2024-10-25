using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillMagic : MonoBehaviour
{
    Rigidbody2D _rigibody2d;
    BossHealth Boss;
    [SerializeField] PlayerConfig _playerConfig;
    private void Awake()
    {
        _rigibody2d = GetComponent<Rigidbody2D>();
        Boss = FindObjectOfType<BossHealth>();
    }
    private void Start()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3 direction = (mousePosition - transform.position).normalized;
        _rigibody2d.velocity = direction * 10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemiesController>().EnemyTakeDamge(_playerConfig.DamgeAp);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            Boss.TakeDamage((int)_playerConfig.DamgeAp);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject, 15f);
        }
    }
}
