using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] float damageTrap;
    [SerializeField] Transform _Attack_Point;
    [SerializeField] Vector2 _Attack_Area;
    [SerializeField] LayerMask _PlayerLayer;
    [SerializeField] public float pushForce;
    float nextAttackTime = 0;
    float Attack_Rate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            MakeDamageTrap();
            nextAttackTime = Time.time + 1f / Attack_Rate;
        }
        else return;
    }

    void MakeDamageTrap()
    {
        Collider2D hit_player =
            Physics2D.OverlapBox(_Attack_Point.position, _Attack_Area, 0f, _PlayerLayer);
        if (hit_player != null)
        {
            Vector2 pushDirection = (hit_player.transform.position - transform.position);
            pushDirection.y += 1f;
            pushDirection.Normalize();
            hit_player.GetComponentInParent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            hit_player.GetComponentInParent<PlayerController>().PlayerTakeDamge(damageTrap);
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(_Attack_Point.position, _Attack_Area);
    }
}
