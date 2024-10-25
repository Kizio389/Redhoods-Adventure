using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapUp : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 Position;

    [SerializeField] Transform Point;
    [SerializeField] Vector2 Range;
    [SerializeField] LayerMask playerLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Position = transform.localPosition;

    }
    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 pushDirection = (collision.transform.position - transform.position);
            pushDirection.y += 1f;
            pushDirection.Normalize();
            collision.GetComponentInParent<Rigidbody2D>().AddForce(pushDirection * 5, ForceMode2D.Impulse);
            collision.GetComponentInParent<PlayerController>().PlayerTakeDamge(10f);
            DestroyGameobject();
        }
        else
        {
            if(rb.gravityScale > 0f)
            {
                DestroyGameobject();
            }
        }
    }

    void CheckPlayer()
    {
        Collider2D player = Physics2D.OverlapBox(Point.position, Range, 0f, playerLayer);
        Debug.Log(player);
        if (player != null)
        {
            rb.gravityScale = 1f;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(Point.position, Range);
    }

    void DestroyGameobject()
    {
        gameObject.SetActive(false);
        Invoke(nameof(SpawnGameobject),6);
    }

    void SpawnGameobject()
    {
        rb.gravityScale = 0f;
        gameObject.transform.localPosition = Position;
        gameObject.SetActive(true);
    }
}
