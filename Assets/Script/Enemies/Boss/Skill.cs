using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Skill : MonoBehaviour
{
    Rigidbody2D _rigibody2d;
    Transform positionPlayer;
    [SerializeField] Transform _AttackPoint;
    [SerializeField]  float Damage = 30;
    [SerializeField] float _speed;

    private void Awake()
    {
        positionPlayer = FindObjectOfType<PlayerController>().transform;
        _rigibody2d = GetComponent<Rigidbody2D>();
        _AttackPoint = FindObjectOfType<Transform>();
    }
    private void Start()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        Vector2 playerPos = positionPlayer.transform.position;
        Vector2 bulletPos = _AttackPoint.transform.position;
        Vector2 direction = playerPos - bulletPos;

        _rigibody2d.velocity = direction.normalized * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindAnyObjectByType<PlayerController>().PlayerTakeDamge(Damage);
            FindAnyObjectByType<PlayerMovement>().isStun = true;
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject, 5f);
        }
    }
}
