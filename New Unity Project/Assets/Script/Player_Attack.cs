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
    private float SlideSpeed;

    private Animator PlayerAC;
    public UnityChan_Move UnityChanMove;
    private Rigidbody rigi;
    private Vector3 movement;
    private Vector3 attackMovement;
    
    private Coroutine Canattack ;
    private Coroutine Press_Interval;

    public bool attackB;
    public bool attackABB;
    public bool attackAAB;
    public bool attackAABB;
    public bool slide;

    public ParticleSystem JumpAttack_Particle;
    public ParticleSystem BackAttack_Particle;
   // public ParticleSystem Attack_B_Particle;

    private GameObject Attack_B_obj;
    private GameObject Attack_ABB_obt;
    private GameObject Attack_AAB_obt;
    private GameObject Attack_AABB_obt;

    private void Awake()
    {
        PlayerAC = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody>();
        playerAttack = this;

    }
   private void Start()
    {
        CanAttack = true;
        IsAttack = false;
        CanPress = true;
        attackB = false;
        attackAAB = false;
        slide = false;
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
        if (slide == true)
        {
            slide = false;
        }
       
        CanAttack = true;
        IsAttack = false;
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
            
           
           
            transform.position = Vector3.Lerp(transform.position, movement, Time.deltaTime * SlideSpeed);
           
        }
        else if (Now_State == "Attack_AB")
        {

            transform.position = Vector3.Lerp(transform.position, movement, Time.deltaTime * backSpeed);
        }
        else if (attackB)
        {

            Attack_B_obj.transform.position = Vector3.Lerp(Attack_B_obj.transform.position, attackMovement, Time.deltaTime * attackSpeed);
            if (Attack_B_obj.transform.position == movement)
            {
                attackB = false;
            }
            
        }
        else if (attackABB)
        {
            attackSpeed = 2f;
            Attack_ABB_obt.transform.position = Vector3.Lerp(Attack_ABB_obt.transform.position, attackMovement, Time.deltaTime * attackSpeed);
            Debug.Log(movement);
            if (Attack_ABB_obt.transform.position == movement)
            {
                attackABB = false;
            }
        }
        else if (attackAABB)
        {
            Attack_AABB_obt.transform.position = Vector3.Lerp(Attack_AABB_obt.transform.position, attackMovement, Time.deltaTime * attackSpeed);
            if (Attack_AABB_obt.transform.position == movement)
            {
                attackAABB = false;
            }
        }
    }



    public void Attack_A()
    {
        
        
        if (Count == 0 && CanAttack && !UnityChanMove.IsJumping && CanPress)
        {
            
            StopCoroutine("Cancel_Attack");
            StartCoroutine("Cancel_Attack");
            
            IsAttack = true;
            CanPress = false;


            if (Now_State == null)
            {               
                Now_State = "Attack_A";
                AnimExitTime = 0.8f;
                CanPress_Interval = 0.3f;

                PlayerAC.SetTrigger(Now_State);
            }
            else
            {
                Now_State += "A";
                CanPress_Interval = 0.5f;
                if (Now_State == "Attack_AAA")
                {
                    StopCoroutine(Canattack);
                    AnimExitTime = 2f;

                    JumpAttack_Particle.Stop();
                    JumpAttack_Particle.Play();
                }
                else
                {
                    StopCoroutine(Canattack);
                    AnimExitTime = 1;
                }
               
                PlayerAC.SetTrigger(Now_State);
                
                if (Now_State == "Attack_AAAA")
                {
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
        if (CanAttack && !UnityChanMove.IsJumping && CanPress)
        {
            CanPress = false;
            IsAttack = true;
            if (Now_State == null)
            {
                Now_State = "Attack_B";
                PlayerAC.SetTrigger(Now_State);
                Attack_B_();
                CanPress_Interval = 0.3f;
                

                Now_State = null;
            }
            else
            {
                StopCoroutine("Cancel_Attack");
                StartCoroutine("Cancel_Attack");
                Now_State += "B";
                Count += 1;
                CanPress_Interval = 0.5f;

                if (Now_State == "Attack_AB"|| Now_State == "Attack_ABB")
                {
                    StopCoroutine(Canattack);
                    AnimExitTime = 0.5f;
                    if(Now_State == "Attack_AB")
                    {
                        movement = transform.position - transform.forward * 8;
                        Debug.Log(movement);
                        BackAttack_Particle.Stop();
                        BackAttack_Particle.Play();
                    }
                    else if(Now_State == "Attack_ABB")
                    {
                        Attack_ABB();

                    }

                }
                else
                {
                    if(Now_State == "Attack_AAB")
                    {
                        Attack_AAB();
                        CanPress_Interval = 0.3f;
                    }
                    
                    else if(Now_State == "Attack_AABB")
                    {
                        Attack_AABB();

                    }



                    StopCoroutine(Canattack);
                    AnimExitTime = 0.8f;
                }

                PlayerAC.SetTrigger(Now_State);
                if (Count == 2 || Now_State == "Attack_AAAB")
                {    
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
        /*Attack_B_Particle.Stop();
        Attack_B_Particle.Play();*/
        attackB = true;

        Attack_B_obj = ObjectPool.objectPool.GetAttack_B();

        if (Attack_B_obj == null) return;


        Attack_B_obj.transform.position = transform.position + transform.forward * 1+new Vector3(0,0.9f,0);
        Attack_B_obj.SetActive(true);
        attackMovement = Attack_B_obj.transform.position + transform.forward * 5;
        

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
       

        Attack_AABB_obt.transform.position = transform.position + transform.forward * 0.5f + new Vector3(0, 2, 0);
        Attack_AABB_obt.transform.rotation = transform.rotation;
        Attack_AABB_obt.SetActive(true);
        attackMovement = Attack_AABB_obt.transform.position + transform.forward *6;
        
    }

    private void Attack_ABB()
    {
        attackABB = true;
        Attack_ABB_obt = ObjectPool.objectPool.GetAttack_ABB();

        if (Attack_ABB_obt == null) return;
        Debug.Log(Attack_ABB_obt.transform.position);

        Attack_ABB_obt.transform.position = transform.position + transform.forward * 0.5f + new Vector3(0, 0.9f, 0);
        Attack_ABB_obt.SetActive(true);
        attackMovement = Attack_ABB_obt.transform.position + transform.forward * 10;
       
    }

    public void Slide()
    {
        Now_State = null;
       
        CanAttack = false;
        IsAttack = true;
        AnimExitTime = 2.3f;
        Canattack = StartCoroutine(Can_attack(AnimExitTime));
        Press_Interval = StartCoroutine(CanPressed(CanPress_Interval));
        movement = transform.position + transform.forward * 8;
        slide = true;


        PlayerAC.SetTrigger("Slide");

    }

}
