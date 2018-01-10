using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Attack : MonoBehaviour {

    public bool IsAttack;
    private string Now_State;
    private bool CanAttack;
    private int Count = 0;
    private float AnimExitTime;
    private Animator PlayerAC;
    private Animation NowAnimation;


    public UnityChan_Move UnityChanMove;

    private Coroutine Canattack ;

    private void Awake()
    {
        PlayerAC = GetComponent<Animator>();
        NowAnimation = GetComponent<Animation>();
    }
   private void Start()
    {
        CanAttack = true;
        IsAttack = false;
    }

    public IEnumerator Cancel_Attack()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Cancel_Attack");
        Now_State = null; 
        Count = 0;
        
        // PlaerAC.SetTrigger("ExitCombo");
    }

    private IEnumerator Can_attack(float Now_Anim)
    {
        yield return new WaitForSeconds(Now_Anim);
        Debug.Log(Now_Anim);
        CanAttack = true;
        IsAttack = false;
    }

    public void Attack_A()
    {
        

        if (Count == 0 && CanAttack && !UnityChanMove.IsJumping)
        {
            
            StopCoroutine("Cancel_Attack");
            StartCoroutine("Cancel_Attack");
            
            IsAttack = true;

            if (Now_State == null)
            {               
                Now_State = "Attack_A";
                AnimExitTime = 0.8f;
               
                PlayerAC.SetTrigger(Now_State);
            }
            else
            {
                Now_State += "A";

                if (Now_State == "Attack_AAA")
                {
                    StopCoroutine(Canattack);
                    AnimExitTime = 1.5f;
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
                    AnimExitTime = 2;
                  
                }
                
            }
            
                Canattack = StartCoroutine(Can_attack(AnimExitTime));
           
        }
       
    }
    public void Attack_B()
    {
        AnimationClip[] animationClips = PlayerAC.runtimeAnimatorController.animationClips;

        if (CanAttack && !UnityChanMove.IsJumping)
        {
            IsAttack = true;
            if (Now_State == null)
            {
                Now_State = "Attack_B";
                PlayerAC.SetTrigger(Now_State);        
               
                Now_State = null;
            }
            else
            {
                StopCoroutine("Cancel_Attack");
                StartCoroutine("Cancel_Attack");
                Now_State += "B";
                Count += 1;

                if (Now_State == "Attack_AB"|| Now_State == "Attack_ABB")
                {
                    StopCoroutine(Canattack);
                    AnimExitTime = 0.5f;
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

        }
    }
        

}
