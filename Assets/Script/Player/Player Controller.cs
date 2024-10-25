using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Check Grounded")]
    [SerializeField] float Radius_LegPos; //Vùng xác định chạm đất
    public bool _OnGrounded; //Kiểm tra nhân vật có chạm đất hay không
    [SerializeField] private Transform Leg_pos; //Vị trí điểm chạm của nhân vật
    [SerializeField] private LayerMask Ground; //Mặt đất
    private int JumpValue = 2; //Số lần nhảy

    [Header("Attack")]
    [SerializeField] private Transform _AttackPoint;
    [SerializeField] private LayerMask _EnemiesLayer;
    [SerializeField] private float _AttackRange = 0.5f;
    [SerializeField] float pushForce;
    [SerializeField] AudioSource _AttackSound;

    [SerializeField] float Splash_force;

    [Header("Magic")]
    [SerializeField]
    private float _AttackSkillThunderRange;
    [SerializeField]
    private GameObject ThunderSkill;
    [SerializeField] private GameObject Skill_magic;
    [SerializeField] private Transform Magic_pos;
    [SerializeField] AudioSource _ShootSound;

    [Header("Flip")]    
    private bool _IsRight = true; //Kiểm tra nhân vật quay bên phải

    [Header("Info")]
    [SerializeField] Image Hearth_Bar;
    [SerializeField] Image Mana_Bar;
    [SerializeField] TextMeshProUGUI Point_Coin;

    [Header("Dead")]
    [SerializeField] GameObject PanelDead;
    [SerializeField] TextMeshProUGUI timerText;
    private Animator _Animator;
    private Rigidbody2D _rigidbody2D;

    Vector3 PositionLocal;

    DataPlayer dataPlayer;

   [SerializeField] private InventoryItem item;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        dataPlayer = DataPlayer.Instance;
    }

    private void Start()
    {
        PositionLocal = transform.position;
        Point_Coin.text = dataPlayer.Coin.ToString();
        Hearth_Bar.fillAmount = dataPlayer.Health / dataPlayer.Max_Health;
        Mana_Bar.fillAmount = dataPlayer.Energy / dataPlayer.Max_Energy;
    }

    private void Update()
    {
        ReSetJump();
    }

    private void FixedUpdate()
    {
        _OnGrounded = false;
        Collider2D[] collider2d = Physics2D.OverlapCircleAll(Leg_pos.position, Radius_LegPos, Ground);
        for (int i = 0; i < collider2d.Length; i++)
        {
            if (collider2d[i].gameObject != gameObject)
            {
                _OnGrounded = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Leg_pos.position,Radius_LegPos);
    }

    public void Move(float _speed)
    {
        gameObject.transform.position += new Vector3(_speed * 10 * Time.deltaTime, 0, 0);
        if(_speed > 0 && !_IsRight)
        {
            Flip();
        }   
        else if(_speed < 0 && _IsRight)
        {
            Flip();
        }
    }
    
    public void Jump(float _jumpForce)
    {
        if (_OnGrounded == true && JumpValue > 0)
        {
            _rigidbody2D.AddForce(new Vector2(0, _jumpForce));
            JumpValue--;
        }
        else if (_OnGrounded == false && JumpValue > 0)
        {
            _rigidbody2D.AddForce(new Vector2(0, _jumpForce));
            JumpValue--;
        }
        else if (_OnGrounded == false && JumpValue <= 0)
        {
            _rigidbody2D.AddForce(new Vector2(0, 0));
        }
    }
    void ReSetJump()
    {
        if(_OnGrounded == true && JumpValue == 0)
        {
            JumpValue += 2;
        }
    }

    public void Attack()
    {
        _AttackSound.Play();
        //Kiểm tra kiếm có chạm vào quái hay chưa
        Collider2D[] hit_Enemies = Physics2D.OverlapCircleAll(_AttackPoint.position, _AttackRange, _EnemiesLayer);
        foreach(Collider2D enemy in hit_Enemies)
        {
            if (enemy != null)
            {
                Vector2 pushDirection = (enemy.transform.position - transform.position);
                pushDirection.y += 1f;
                pushDirection.Normalize();
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                    enemy.GetComponent<EnemiesController>().EnemyTakeDamge(dataPlayer.AD_Damage);
                }
                else if (enemy.CompareTag("Boss"))
                {
                    enemy.GetComponent<BossHealth>().TakeDamage((int)dataPlayer.AD_Damage);
                }
            }
        }
    }

    public void Skill_Attack()
    {
        
            if (_IsRight == true)
            {
                //_rigidbody2D.AddForce(new Vector2(Splash_force, 0));
                _rigidbody2D.velocity = new Vector2(Splash_force, 0);
            }
            else
            {
                //_rigidbody2D.AddForce(new Vector2(-Splash_force, 0));
                _rigidbody2D.velocity = new Vector2(-Splash_force, 0);
            }
            dataPlayer.Energy -= 10;
            Mana_Bar.fillAmount = dataPlayer.Energy / dataPlayer.Max_Energy;
            _Animator.SetTrigger("Skill_Attack");
    }

    public void Shoot_Magic()
    {
        if (dataPlayer.Energy > 0)
        {
            _ShootSound.Play();
            StartCoroutine("ShootMagic");
        }
        else
        {
            dataPlayer.Energy = 0;
        }
    }
    public IEnumerator ShootMagic()
    {
        dataPlayer.Energy -= 10;
        Debug.Log(dataPlayer.Energy);
        Mana_Bar.fillAmount = dataPlayer.Energy / dataPlayer.Max_Energy;
        Instantiate(Skill_magic, Magic_pos.position, Quaternion.identity);
        yield return null;
    }

    public void PlayerTakeDamge(float _damge)
    {
        if(_Animator.GetBool("Dead") == false)
        {
            _Animator.SetTrigger("Hurt");
            dataPlayer.Health -= _damge;
            Hearth_Bar.fillAmount = dataPlayer.Health / dataPlayer.Max_Health;
            if (dataPlayer.Health <= 0)
            {
                _Animator.SetBool("Dead", true);
                PlayerDie();
            }
        }
    }

    //Hiển thị vùng tấn công
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_AttackPoint.position, _AttackRange);
        Gizmos.DrawWireSphere(_AttackPoint.position, _AttackSkillThunderRange);
    }

    void Flip()
    {
        _IsRight = !_IsRight;
        //Debug.Log(_IsRight);
        transform.Rotate(0, 180, 0);
    }    

    void PlayerDie()
    {
        dataPlayer.Health = 0;
        PanelDead.SetActive(true);
        GetComponent<PlayerMovement>().enabled = false;
        StartCoroutine(RespawnPlayer());
    }

    IEnumerator RespawnPlayer()
    {
        int timeRespawn = 5;
        while (timeRespawn >= 0)
        {
            UpdateTimerUI(timeRespawn);
            yield return new WaitForSeconds(1f);
            timeRespawn -= 1;
            if(timeRespawn <= 0)
            {
                _Animator.SetBool("Dead", false);
                PanelDead.SetActive(false);
                GetComponent<PlayerMovement>().enabled = true;
                dataPlayer.Health = dataPlayer.Max_Health;
                dataPlayer.Energy = dataPlayer.Max_Energy;
                Hearth_Bar.fillAmount = dataPlayer.Health / dataPlayer.Max_Health;
                Mana_Bar.fillAmount = dataPlayer.Energy / dataPlayer.Max_Energy;
                gameObject.transform.position = PositionLocal;
            }
        }

    }
    private void UpdateTimerUI(int timer)
    {
        timerText.text = "You Death!" + "\nRespawn: " + Mathf.FloorToInt(timer).ToString();
    }

    public void SetCoin()
    {
        dataPlayer.Coin++;
        Point_Coin.text = dataPlayer.Coin.ToString();
    }

    public void Healing(float index, string name)
    {
        if(name == "HP")
        {
            dataPlayer.Health += index;
            Hearth_Bar.fillAmount = dataPlayer.Health / dataPlayer.Max_Health;
        } 
        else if(name == "MP")
        {
            dataPlayer.Energy += index;
            Mana_Bar.fillAmount = dataPlayer.Energy / dataPlayer.Max_Energy;
        }
    }

    public void LevelUp(int index)
    {
        dataPlayer.EXP += index;
        if(dataPlayer.EXP >= dataPlayer.Max_EXP)
        {
            dataPlayer.Level++;
            dataPlayer.SkillPoint++;
            SetEXP();
            dataPlayer.Health = dataPlayer.Max_Health;
            Hearth_Bar.fillAmount = dataPlayer.Health / dataPlayer.Max_Health;
            dataPlayer.Energy = dataPlayer.Max_Energy;
            Mana_Bar.fillAmount = dataPlayer.Energy / dataPlayer.Max_Energy;

        }
    }
    public void SetEXP()
    {
        int temp;
        temp = dataPlayer.EXP - dataPlayer.Max_EXP;
        dataPlayer.EXP = temp;
        dataPlayer.Max_EXP += (dataPlayer.Max_EXP * 30 / 100);
    }

    internal void Skill_Thunder()
    {
        if (dataPlayer.Energy > 0)
        {
            Collider2D[] hit_Enemies = Physics2D.OverlapCircleAll(_AttackPoint.position, _AttackSkillThunderRange, _EnemiesLayer);
            foreach (Collider2D enemy in hit_Enemies)
            {
                if (enemy != null)
                {
                    Instantiate(ThunderSkill, enemy.transform.position, Quaternion.identity);
                    dataPlayer.Energy -= 10;
                    Mana_Bar.fillAmount = dataPlayer.Energy / dataPlayer.Max_Energy;
                }
            }
        }
        else
        {
            dataPlayer.Energy = 0;
        }
        
    }
}
