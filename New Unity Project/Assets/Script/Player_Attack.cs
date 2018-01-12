using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Attack : MonoBehaviour {
    public static Player_Attack playerAttack;


    public bool IsAttack;
    private string Now_State;
    public bool CanAttack;
    private int Count = 0;
    private float AnimExitTime;
    public bool CanPress;
    private float CanPress_Interval;
    public float backSpeed;
    public float attackSpeed;
    public float SlideSpeed;

    private Animator PlayerAC;
    public UnityChan_Move UnityChanMove;
    private Rigidbody rigi;
    private Vector3 movement;
    private Vector3 attackMovement;
    private CapsuleCollider PlayerCollider;
    public BoxCollider AAA_collider;
    public BoxCollider AAAA_collider;
    public PlayerHealth playerHealth;

    public BoxCollider leftLeg;
    public BoxCollider rightHand;

    int Enemy;

    private Coroutine Canattack ;
    private Coroutine Press_Interval;


    public bool attackAA;
    public bool attackB;
    public bool attackABB;
    public bool attackAAB;
    public bool attackAABB;
    public bool attackAAAB;
    public bool slide;

    public ParticleSystem JumpAttack_Particle;
    public ParticleSystem BackAttack_Particle;
    public ParticleSystem Attack_AAAA_Particle;

    private GameObject Attack_B_obj;
    private GameObject Attack_ABB_obt;
    private GameObject Attack_AAB_obt;
    private GameObject Attack_AABB_obt;
    private GameObject Attack_AAAB_obt;

    private AudioSource Audio;
    public AudioClip attacka;
    public AudioClip attackaa;
    public AudioClip attackab;
    public AudioClip attackabb;
    public AudioClip attackAAA;
    public AudioClip attackaab;
    public AudioClip attackaabb;
    public AudioClip attackaaaa;
    public AudioClip attackaaab;

    public void Awake()
    {
        PlayerAC = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody>();
        PlayerCollider = GetComponent<CapsuleCollider>();
        Audio = GetComponent<AudioSource>();
        AAAA_collider.enabled = false;
        AAA_collider.enabled = false;

        playerAttack = this;
    }
   private void Start()
    {
        CanAttack = true;
        IsAttack = false;
        CanPress = true;
        rightHand.enabled = false;
        leftLeg.enabled = false;
        attackAA = false;
        attackB = false;
        attackABB = false;
        attackAAB = false;
        attackAABB = false;
        attackAAAB = false;
        slide = false;
        Enemy = LayerMask.GetMask("Enemy");
    }

    public IEnumerator Cancel_Attack()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Cancel_Attack");
        Now_State = null; 
        Count = 0;
        
        
    }

    private IEnumerator Can_attack(float Now_Anim)
    {
        yield return new WaitForSeconds(Now_Anim);
        if (slide || attackB|| attackABB||attackAAB||attackAABB||attackAAAB||attackAA)
        {
            slide = false;
            attackB = false;
            attackABB = false;
            attackAAB = false;
            attackAABB = false;
            attackAAAB = false;
            attackAA = false;
            
        }
       
        CanAttack = true;
        IsAttack = false;
        AAA_collider.enabled = false;
        AAAA_collider.enabled = false;
        rightHand.enabled = false;
        leftLeg.enabled = false;
    }
    private IEnumerator CanPressed(float PressInterval)
    {
        yield return new WaitForSeconds(PressInterval);

        CanPress = true;
    }

    private void FixedUpdate()
    {
       
        if (slide)
        {
            PlayerCollider.height = PlayerAC.GetFloat("colli_Height");
            PlayerCollider.center = new Vector3(0, PlayerAC.GetFloat("colli_pos_y"), PlayerAC.GetFloat("colli_pos_z"));
           
            transform.position = Vector3.Lerp(transform.position, movement, Time.deltaTime * PlayerAC.GetFloat("Speed"));
            

        }
        else if (Now_State == "Attack_AB")
        {

            transform.position = Vector3.Lerp(transform.position, movement, Time.deltaTime * backSpeed);
        }
       
        else if (attackB)
        {
            attackSpeed = 2f;
            Attack_B_obj.transform.position = Vector3.Lerp(Attack_B_obj.transform.position, attackMovement, Time.deltaTime * attackSpeed);
           
           
            
        }
        else if (attackABB)
        {
            attackSpeed = 2f;
            Attack_ABB_obt.transform.position = Vector3.Lerp(Attack_ABB_obt.transform.position, attackMovement, Time.deltaTime * attackSpeed);
          //  Debug.Log(movement);
          
        }
        else if (attackAABB)
        {
            attackSpeed = 5f;
            Attack_AABB_obt.transform.position = Vector3.Lerp(Attack_AABB_obt.transform.position, attackMovement, Time.deltaTime * attackSpeed);
         
        }
        else if (attackAAAB)
        {
            attackSpeed = 3f;
            Attack_AAAB_obt.transform.position = Vector3.Lerp(Attack_AAAB_obt.transform.position, attackMovement, Time.deltaTime * attackSpeed);
           
        }
        

    }
    
    public void Attack_A()
    {
         if (Count == 0 && CanAttack && !UnityChanMove.IsJumping && CanPress && !playerHealth.isDead)
        {
            
            StopCoroutine("Cancel_Attack");
            StartCoroutine("Cancel_Attack");
            
            IsAttack = true;
            CanPress = false;


            if (Now_State == null)
            {
                Audio.clip = attacka;
                Audio.Play();
                Now_State = "Attack_A";
                AnimExitTime = 0.8f;
                CanPress_Interval = 0.3f;
                leftLeg.enabled = true;
                PlayerAC.SetTrigger(Now_State);

            }
            else
            {
                Now_State += "A";
                CanPress_Interval = 0.5f;

                if(Now_State == "Attack_AA")
                {
                    Audio.clip = attackaa;
                    Audio.Play();
                    rightHand.enabled = true;
                }

                else if (Now_State == "Attack_AAA")
                {
                    Audio.clip = attackAAA;
                    Audio.Play();
                    AAA_collider.enabled = true;
                    AnimExitTime = 2f;
                    CanPress_Interval = 0.8f;
                    JumpAttack_Particle.Stop();
                    JumpAttack_Particle.Play();
                }
                else
                {
                   
                    AnimExitTime = 1;
                }
               
                PlayerAC.SetTrigger(Now_State);
                
                if (Now_State == "Attack_AAAA")
                {
                    Audio.clip = attackaaaa;
                    Audio.Play();
                    Attack_AAAA_Particle.Stop();
                    Attack_AAAA_Particle.Play();

                    AAAA_collider.enabled = true;
                    CanAttack = false;
                    PlayerAC.SetTrigger(Now_State);
                    Now_State = null;
                    StopCoroutine("Cancel_Attack");
                    StopCoroutine(Canattack);
                    AnimExitTime = 2;
                  
                }
                
            }
            
            Canattack = StartCoroutine(Can_attack(AnimExitTime));
            Press_Interval = StartCoroutine(CanPressed(CanPress_Interval));
        }
       
    }
    public void Attack_B()
    {      
        if (CanAttack && !UnityChanMove.IsJumping && CanPress && !playerHealth.isDead)
        {
            CanPress = false;
            IsAttack = true;
            //CanAttack = false;
            if (Now_State == null)
            {
                Now_State = "Attack_B";
                PlayerAC.SetTrigger(Now_State);
                Attack_B_();
                CanPress_Interval = 0.3f;
                AnimExitTime = 0.5f;

                Now_State = null;
            }
            else
            {
                StopCoroutine("Cancel_Attack");
                StartCoroutine("Cancel_Attack");
                Now_State += "B";
                Count += 1;
                CanPress_Interval = 0.5f;

                   
                    AnimExitTime = 0.5f;
                    if(Now_State == "Attack_AB")
                    {
                        Audio.clip = attackab;
                        Audio.Play();
                        PlayerAC.SetTrigger(Now_State);
                        movement = transform.position - transform.forward * 8;
                       // Debug.Log(movement);
                        BackAttack_Particle.Stop();
                        BackAttack_Particle.Play();
                    }
                    else if(Now_State == "Attack_ABB")
                    {
                        Audio.clip = attackabb;
                        Audio.Play();
                        PlayerAC.SetTrigger(Now_State); 
                        Attack_ABB();
                        Count = 0;
                        Now_State = null;
                    }

                
                    else if(Now_State == "Attack_AAB")
                    {
                        Audio.clip = attackaab;
                        Audio.Play();
                        PlayerAC.SetTrigger(Now_State);
                        Attack_AAB();
                        CanPress_Interval = 0.8f;
                        AnimExitTime = 2f;
                    }
                    
                    else if(Now_State == "Attack_AABB")
                    {
                        Audio.clip = attackaabb;
                        Audio.Play();
                        PlayerAC.SetTrigger(Now_State);
                        Attack_AABB();
                        Count = 0;
                        Now_State = null;
                    }

                
                if (Now_State == "Attack_AAAB")
                {
                    Audio.clip = attackaaab;
                    Audio.Play();
                    PlayerAC.SetTrigger(Now_State);
                    Attack_AAAB();
                    Count = 0;            
                    StopCoroutine(Canattack);
                    AnimExitTime =2.3f;

                    StopCoroutine("Cancel_Attack");                
                    Now_State = null;
                }

            }
            
            Canattack = StartCoroutine(Can_attack(AnimExitTime));
            Press_Interval = StartCoroutine(CanPressed(CanPress_Interval));
        }
    }
        
    private void Attack_B_()
    {   
        attackB = true;
        Attack_B_obj = ObjectPool.objectPool.GetAttack_B();

        if (Attack_B_obj == null) return;


        Attack_B_obj.transform.position = transform.position + transform.forward * 1+new Vector3(0,0.9f,0);
        Attack_B_obj.SetActive(true);
        attackMovement = Attack_B_obj.transform.position + transform.forward *8;
        

    }
    private void Attack_ABB()
    {
        attackABB = true;
        Attack_ABB_obt = ObjectPool.objectPool.GetAttack_ABB();

        if (Attack_ABB_obt == null) return;
       // Debug.Log(Attack_ABB_obt.transform.position);

        Attack_ABB_obt.transform.position = transform.position + transform.forward * 0.5f + new Vector3(0, 0.9f, 0);
        Attack_ABB_obt.SetActive(true);
        attackMovement = Attack_ABB_obt.transform.position + transform.forward * 7;

    }
    private void Attack_AAB()
    {
        attackAAB = true;
        Attack_AAB_obt = ObjectPool.objectPool.GetAttack_AAB();

        if (Attack_AAB_obt == null) return;

        Attack_AAB_obt.transform.rotation = transform.rotation;
        Attack_AAB_obt.transform.position = transform.position + new Vector3(0, 0.9f, 0);
        Attack_AAB_obt.SetActive(true);
        
       
    }
    private void Attack_AABB()
    {
        attackAABB = true;
        Attack_AABB_obt = ObjectPool.objectPool.GetAttack_AABB();

        if (Attack_AABB_obt == null) return;

        Attack_AABB_obt.transform.rotation = transform.rotation;
        Attack_AABB_obt.transform.position = transform.position + transform.forward * 0.1f + new Vector3(0, 2, 0);
        
        Attack_AABB_obt.SetActive(true);
        attackMovement = Attack_AABB_obt.transform.position + transform.forward *8;
        
    }
    private void Attack_AAAB()
    {
        attackAAAB = true;
        Attack_AAAB_obt = ObjectPool.objectPool.GetAttack_AAAB();

        if (Attack_AAAB_obt == null) return;


        Attack_AAAB_obt.transform.position = transform.position + transform.forward * 1f + new Vector3(0, 2, 0);
       
        Attack_AAAB_obt.SetActive(true);
        attackMovement = Attack_AAAB_obt.transform.position + transform.forward * 10;

    }


    public void Slide()
    {
        if (!slide && !playerHealth.isDead)
        {
            Now_State = null;

            CanAttack = false;
            IsAttack = true;
            AnimExitTime = 1.3f;
            Canattack = StartCoroutine(Can_attack(AnimExitTime));
            Press_Interval = StartCoroutine(CanPressed(CanPress_Interval));
            movement = transform.position + transform.forward * 8;
            slide = true;

            PlayerAC.SetTrigger("Slide");
        }
        

    }

}
