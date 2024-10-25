using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;

    public Vector3 attackOffset;
    public float attackRange;
    public LayerMask attackMask;

    [SerializeField] GameObject skillMagic;
    [SerializeField] Transform posSkill;
    [SerializeField] GameObject Player;
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponentInParent<PlayerController>().PlayerTakeDamge(attackDamage);
        }
    }

    public void Attack3()
    {
        StartCoroutine(SpawnSkillMagic());
    }

    private IEnumerator SpawnSkillMagic()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(skillMagic, posSkill.position, Quaternion.identity);
            yield return new WaitForSeconds(2f); // Chờ 2 giây trước khi sinh ra skillMagic tiếp theo
        }
        StartCoroutine(Attack1());
    }

    private IEnumerator Attack1()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<Animator>().SetTrigger("Tele");
        Vector3 currentPosition = Player.transform.position;
        currentPosition.x -= 3f;
        gameObject.transform.position = currentPosition;
        GetComponent<Animator>().SetTrigger("Attack1");
    }

    public void EnragedAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().PlayerTakeDamge(enragedAttackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
