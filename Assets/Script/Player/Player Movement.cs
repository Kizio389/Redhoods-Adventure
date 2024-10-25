using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerController _PlayerController;
    [SerializeField] private ButtonHeal[] _Heal;

    [SerializeField] private float Speed; // Tốc độ di chuyển
    [SerializeField] private float _JumpForce; //Lực nhảy
    [SerializeField] public bool isStun = false;

    //Cooldown attack
    [Header("")]
    [SerializeField] private float AttackSword_Rate;
    [SerializeField] private float nextAttackSwordTime = 0f;

    [Header("")]
    [SerializeField] private float AttackMagic_Rate;
    [SerializeField] private float nextAttackMagicTime = 0f;

    [Header("")]
    [SerializeField] private float AttackThunder_Rate;
    [SerializeField] private float nextAttackThunderTime = 0f;

    [Header("")]
    [SerializeField] private float AttackSplash_Rate;
    [SerializeField] private float nextAttackSplashTime = 0f;

    [Header("")]
    [SerializeField] Image imageSkillMagic;
    [SerializeField] Image imageSkillThunder;
    [SerializeField] Image imageSkillSplash;

    [Header("")]
    [SerializeField] float nextStunTime = 0f;
    [SerializeField] float Stun_rate;
    
    [Header("Animation")]
    private Animator _Animator;

    DataPlayer dataPlayer;
    private void Awake()
    {
        dataPlayer = DataPlayer.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        _PlayerController = GetComponent<PlayerController>();
        _Animator = GetComponent<Animator>();
        _Animator.SetBool("OnGround", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isStun == true) { Non_Stun(); }
        Move();
        Jump();
        OnAttack();
        UpdateUIMagicSkill();
        UpdateUIThunderSkill();
        UpdateUISplashSkill();
        //Healing();
    }
    
    void Move()
    {
        if(isStun == true) { _Animator.SetBool("Stun", isStun); return; }
        if (Input.GetKey(KeyCode.D))
        {
            _PlayerController.Move(Speed);
            _Animator.SetFloat("Ani_Speed", 1);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            _PlayerController.Move(-Speed);
            _Animator.SetFloat("Ani_Speed", 1);
        }
        else
        {
            _Animator.SetFloat("Ani_Speed", 0);
        }    
    }
    
    void Jump()
    {
        if (isStun == true) { _Animator.SetBool("Stun", isStun); return; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _PlayerController.Jump(_JumpForce);  
        }
        _Animator.SetBool("OnGround", _PlayerController._OnGrounded);
    }

    void OnAttack()
    {
        if (isStun == true) { _Animator.SetBool("Stun", isStun); return; }
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time >= nextAttackSwordTime)
            {
                _Animator.SetTrigger("Attack");
                nextAttackSwordTime = Time.time + 1f / AttackSword_Rate; //Cộng cooldown attack
            }
            else return;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (Time.time >= nextAttackSplashTime)
            {
                if (dataPlayer.Health > 0)
                {
                    imageSkillSplash.fillAmount = 0;
                    nextAttackSplashTime = Time.time + 1f / AttackSplash_Rate;
                    _Animator.SetTrigger("LastSkillAttack");
                }
                else
                {
                    dataPlayer.Health = 0;
                }
            }
            else return;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (Time.time >= nextAttackThunderTime)
            {
                imageSkillThunder.fillAmount = 0;
                _PlayerController.Skill_Thunder();
                nextAttackThunderTime = Time.time + 1f / AttackThunder_Rate;
            }
            else return;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.time >= nextAttackMagicTime)
            {
                imageSkillMagic.fillAmount = 0;
                _PlayerController.Shoot_Magic();
                nextAttackMagicTime = Time.time + 1f / AttackMagic_Rate; //Cộng cooldown attack
            }
            else return;

        }
    }

    void Non_Stun()
    {
        if (Time.time >= nextStunTime)
        {
            nextStunTime = Time.time + 1f / Stun_rate; 
            //Destroy(Stun_effect);
            isStun = false;
            _Animator.SetBool("Stun", isStun);
        }
    }

    void UpdateUIMagicSkill()
    {
        // Tính toán thời gian còn lại cho hồi chiêu
        float remainingCooldown = nextAttackMagicTime - Time.time;

        // Tính toán tỷ lệ fill dựa trên thời gian còn lại cho hồi chiêu
        float fillRatio = Mathf.Clamp01(1f - (remainingCooldown / (1f / AttackMagic_Rate)));

        // Áp dụng tỷ lệ fill vào hình ảnh
        imageSkillMagic.fillAmount = fillRatio;

    }
    void UpdateUIThunderSkill()
    {
        // Tính toán thời gian còn lại cho hồi chiêu
        float remainingCooldown = nextAttackThunderTime - Time.time;

        // Tính toán tỷ lệ fill dựa trên thời gian còn lại cho hồi chiêu
        float fillRatio = Mathf.Clamp01(1f - (remainingCooldown / (1f / AttackThunder_Rate)));

        // Áp dụng tỷ lệ fill vào hình ảnh
        imageSkillThunder.fillAmount = fillRatio;

    }
    void UpdateUISplashSkill()
    {
        // Tính toán thời gian còn lại cho hồi chiêu
        float remainingCooldown = nextAttackSplashTime - Time.time;

        // Tính toán tỷ lệ fill dựa trên thời gian còn lại cho hồi chiêu
        float fillRatio = Mathf.Clamp01(1f - (remainingCooldown / (1f / AttackSplash_Rate)));

        // Áp dụng tỷ lệ fill vào hình ảnh
        imageSkillSplash.fillAmount = fillRatio;

    }
}
