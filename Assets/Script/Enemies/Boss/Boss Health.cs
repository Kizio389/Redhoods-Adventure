using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int health = 1000;
    public int current_health;

    int indexTakeDamage = 3 ;

    public GameObject deathEffect;

    public bool isInvulnerable = false;

    [SerializeField] Image healthBar;

    private void Start()
    {
        current_health = health;
        healthBar.fillAmount = (float)current_health / health;
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;
        indexTakeDamage--;
        if (indexTakeDamage < 0)
        {
            Teleport();
            indexTakeDamage = 3;
        }
        GetComponent<Animator>().SetTrigger("Hurt");
        current_health -= damage;
        healthBar.fillAmount = (float)current_health / health;
        if (current_health <= 200)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }

        if (current_health <= 0)
        {
            Die();
        }
    }

    public void Teleport()
    {
        GetComponent<Animator>().SetTrigger("Tele");
        // Lấy kích thước của màn hình trong pixel
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Tạo một điểm ngẫu nhiên trên màn hình
        Vector3 randomPosition = new Vector3(Random.Range(0, screenWidth), Random.Range(0, screenHeight), 0);

        // Chuyển đổi điểm ngẫu nhiên từ tọa độ pixel sang tọa độ của thế giới trong game
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(randomPosition);

        // In ra vị trí ngẫu nhiên trong thế giới của game
        gameObject.transform.position = worldPosition;
        GetComponent<Animator>().SetTrigger("Attack3");
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
