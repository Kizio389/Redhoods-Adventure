using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D rb;
    Transform player;
    [SerializeField] AudioSource soundEffect;
    private float flyingSpeed = 9f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartSpawn();
    }

    private void Update()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        //Debug.Log("Coin");
        //Vector3 target = new Vector3(player.position.x,player.position.y,rb.position.y);
        //Vector3 newPos = Vector3.MoveTowards(rb.position, target, flyingSpeed);
        //rb.MovePosition(newPos);

        Vector2 playerPos = player.transform.position;
        Vector2 coinPos = transform.position;
        Vector2 direction = playerPos - coinPos;

        rb.velocity = direction.normalized * flyingSpeed;
    }

    void StartSpawn()
    {
        float randX = Random.Range(2, 7);
        float randY = Random.Range(2, 7);
        rb.AddForce(new Vector2(randX, randY));
        rb.velocity = new Vector2(randX, randY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerController>().SetCoin();

            soundEffect.Play();

            StartCoroutine(DestroyAfterSound());
        }
    }
    private IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(soundEffect.clip.length / 9);
        Destroy(gameObject);
    }
}
