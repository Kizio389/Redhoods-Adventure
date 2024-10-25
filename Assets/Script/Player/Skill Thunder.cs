using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThuner : MonoBehaviour
{
    [SerializeField] float pushForce;
    [SerializeField] PlayerConfig Default_playerConfig;
    [SerializeField] Transform PointMakeDamage;
    [SerializeField] LayerMask _EnemiesLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
    private void MakeDamage()
    {
        Collider2D hit_Enemies = Physics2D.OverlapPoint(PointMakeDamage.position, _EnemiesLayer);
            if (hit_Enemies != null)
            {
                Vector2 pushDirection = (hit_Enemies.transform.position - gameObject.transform.position);
                pushDirection.y += 1f;
                pushDirection.Normalize();
                if (hit_Enemies.CompareTag("Enemy"))
                {
                hit_Enemies.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                hit_Enemies.GetComponent<EnemiesController>().EnemyTakeDamge(Default_playerConfig.DamgeAp);
                }
                else if (hit_Enemies.CompareTag("Boss"))
                {
                hit_Enemies.GetComponent<BossHealth>().TakeDamage((int)Default_playerConfig.DamgeAp);
                }
            }
        
        
    }
}
