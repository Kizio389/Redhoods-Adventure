using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bettle : EnemiesController
{
    [SerializeField] public float _AttackFlyRange;

    [SerializeField] GameObject Bullet_prefab;
    [SerializeField] Transform pos_Attack;
    [SerializeField] float nextAttackFlyTime;
    [SerializeField] float AttackFly_Rate;
    private void Update()
    {
        OnAttack();
        OnFLyAttack();
        FlipAnimals();
    }

    public void AttackFly()
    {
        //Kiểm tra kiếm có chạm vào player hay chưa
        Collider2D[] hit_Player = Physics2D.OverlapCircleAll(_AttackPoint.position, _AttackFlyRange, _PlayerLayer);
        foreach (Collider2D Player in hit_Player)
        {
            if (Player != null)
            {
                Instantiate(Bullet_prefab, pos_Attack.position, Quaternion.identity);
            }
        }
    }

    void OnFLyAttack()
    {
        if (Time.time >= nextAttackFlyTime)
        {
            AttackFly();
            nextAttackFlyTime = Time.time + 1f / AttackFly_Rate; //Cộng cooldown attack
        }
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawWireSphere(_AttackPoint.position, _AttackFlyRange);
    //}

}
