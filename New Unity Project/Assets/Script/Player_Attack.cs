using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Attack : MonoBehaviour {

    public bool IsAttack;
    private string Now_State;
    public bool CanAttack;
    private int Count = 0;
    private float AnimExitTime;
    public bool CanPress;
    private float CanPress_Interval;
    public float backSpeed;
    public bool attackB;
    public float attackB_Speed;

    private Animator PlayerAC;
    public UnityChan_Move UnityChanMove;

    private Rigidbody rigi;
    private Vector3 movement;
    public Transform Attack_AB_Transform;
    

    private Coroutine Canattack ;
    private Coroutine Press_Interval;

    public ParticleSystem JumpAttack_Particle;
    public ParticleSystem BackAttack_Particle;
   // public ParticleSystem Attack_B_Particle;

    private GameObject Attack_B_obj;

    private void Awake()
    {
        PlayerAC = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody>();
        

    }
   private void Start()
    {
        CanAttack = true;
        IsAttack = false;
        CanPress = true;
        attackB = false;
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
        if (Now_State == "Attack_AB")
        {
            
            transform.position = Vector3.Lerp(transform.position, movement, Time.deltaTime*backSpeed);
        }
        else if (attackB)
        {
           
            Attack_B_obj.transform.position = Vector3.Lerp(Attack_B_obj.transform.position, movement, Time.deltaTime * attackB_Speed);
           
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

                }
                else
                {
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
        movement = Attack_B_obj.transform.position + transform.forward * 5;
        Debug.Log(movement);

    }


}
