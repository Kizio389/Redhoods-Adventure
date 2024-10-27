using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    private Animator _Animator;
    [Header("")]
    [SerializeField] private bool FlyAnimal;

    [Header("")]
    [SerializeField] bool OnGround;
    [SerializeField] LayerMask Ground;
    [SerializeField] Transform Pos_leg;
    [SerializeField] float Radius_check_ground;

    [Header("")]
    [SerializeField] private float _force;
    [SerializeField] private float _speed;

    [Header("")]
    [SerializeField] public float MAX_HP;
    [SerializeField] public float Current_HP;
    [SerializeField] public float Damge;
    [SerializeField] public float pushForce;

    [Header("")]
    [SerializeField] Transform Point_wall;
    [SerializeField] float Radius_check_wall;

    [Header("")]
    bool _isRight = true;
    [SerializeField] float limitMoveRight;
    [SerializeField] float limitMoveLeft;
    [SerializeField] float distanceMoveRight;
    [SerializeField] float distanceMoveLeft;
    float position;

    [Header("")]
    [SerializeField] public Transform _AttackPoint;
    [SerializeField] public float _AttackRange;
    [SerializeField] public LayerMask _PlayerLayer;
    [SerializeField] public float Attack_Rate;
    public float nextAttackTime = 0f;

    [Header("")]
    [SerializeField] float time_Respawn;
    Vector3 PositionSpawn;

    [Header("")]
    [SerializeField] private int indexExp;

    [Header("")]
    [SerializeField] private GameObject Coin;

    [Header("")]
    [SerializeField] AudioSource _SoundTakeDamege;

    //Coin _Coin;
    Rigidbody2D _Rigidbody2D;
    Animator _animator;


    private void Awake()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        //_Coin = GetComponent<Item>();
    }

    private void Start()
    {
        _Animator = GetComponent<Animator>();
        PositionSpawn = gameObject.transform.localPosition;
        Current_HP = MAX_HP;
        position = gameObject.transform.localPosition.x;
        limitMoveRight = position + distanceMoveRight;
        limitMoveLeft = position + distanceMoveLeft;
    }

    private void Update()
    {
        FlipAnimals();
        OnAttack();
    }

    private void FixedUpdate()
    {
        if(FlyAnimal)
        {
            StartCoroutine(InvokeMove());
        }
        else
        {
            _animator.SetBool("OnGround", OnGround);
            Collider2D[] collider2d = Physics2D.OverlapCircleAll(Pos_leg.position, Radius_check_ground, Ground);
            OnGround = false;
            for (int i = 0; i < collider2d.Length; i++)
            {
                if (collider2d[i].gameObject != gameObject)
                {
                    OnGround = true;
                    if (OnGround)
                    {
                        StartCoroutine(InvokeMove());
                    }
                }
            }
        }
    }

    public void EnemyTakeDamge(float damage)
    {
        _SoundTakeDamege.Play();
        Current_HP -= damage;
        _Animator.SetTrigger("Hurt");
        if (Current_HP <= 0)
        {
            _Animator.SetBool("IsDead", true);
            Invoke(nameof(Die), .5f);
        }
    }

    void Die()
    {
        var player = FindObjectOfType<PlayerController>();
        player.LevelUp(indexExp);
        SpawnCoin();
        gameObject.SetActive(false);
        Invoke(nameof(Spawn), time_Respawn);
    }


    public void Attack()
    {
        //Kiểm tra kiếm có chạm vào player hay chưa
        Collider2D[] hit_Player = Physics2D.OverlapCircleAll(_AttackPoint.position, _AttackRange, _PlayerLayer);
        foreach (Collider2D Player in hit_Player)
        {
            if (Player != null)
            {
                Vector2 pushDirection = (Player.transform.position - transform.position);
                pushDirection.y += 1f;
                pushDirection.Normalize();
                Player.GetComponentInParent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                Player.GetComponentInParent<PlayerController>().PlayerTakeDamge(Damge);
            }
        }
    }

    void Spawn()
    {
        gameObject.transform.localPosition = PositionSpawn;
        gameObject.SetActive(true);
        _Animator.SetBool("IsDead", false);
        Current_HP = MAX_HP;
    }


    public void FlipAnimals()
    {
        //Debug.Log(_isRight);
        if (gameObject.transform.localPosition.x >= limitMoveLeft && _isRight == true)
        {
            //Debug.Log("TurnRight");
            Flip(ref _isRight);
        }
        else if(gameObject.transform.localPosition.x <= limitMoveRight && _isRight == false)
        {
            //Debug.Log("TurnLeft");
            Flip(ref _isRight);
        }
        Collider2D[] collider2d = Physics2D.OverlapCircleAll(Point_wall.position, Radius_check_wall, Ground);
        for (int i = 0; i < collider2d.Length; i++)
        {
            if (collider2d[i].gameObject != gameObject)
            {
                Flip(ref _isRight);
            }
        }
    }

    public void Flip(ref bool _IsRight)
    {
        _IsRight = !_IsRight;
        //Debug.Log(_IsRight);
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_AttackPoint.position, _AttackRange);
        Gizmos.DrawWireSphere(Point_wall.position, Radius_check_wall);
        Gizmos.DrawWireSphere(Pos_leg.position, Radius_check_ground);
    }

    IEnumerator InvokeMove()
    {
        yield return new WaitForSeconds(.3f);
        Move(ref _isRight);
    }

    void Move(ref bool _IsRight)
    {
        if (_isRight == true)
        {
            _Rigidbody2D.AddForce(new Vector2(_speed, _force));
        }
        else
        {
            _Rigidbody2D.AddForce(new Vector2(-_speed, _force));
        }
        _animator.SetBool("OnGround", OnGround);
    }

    public void OnAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / Attack_Rate; //Cộng cooldown attack
        }
    }
    public void SpawnCoin()
    {
        var RandomCoin = Random.Range(3, 10);
        int i = 0;
        do
        {
            Instantiate(Coin, gameObject.transform.position, Quaternion.identity);
            i++;
        } while(i < RandomCoin);
    }
}
