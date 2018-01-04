using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour {

    enum ComboState
    {
        Attack_A,
        Attack_AA,
        Attack_AAA,
        Attack_AAAA,

        Attack_AB,
        Attack_ABB,
        Attack_AAB,
        Attack_AABB,
        Attack_AAAB,

        Attack_B,

    }

    ComboState combo_State;

    private string Now_State;
    private int Count = 0;
    private Animator PlaerAC;

    private void Awake()
    {
        PlaerAC = GetComponent<Animator>();
    }
   

    public IEnumerator Cancel_Attack()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Cancel_Attack");
        Now_State = null; 
        Count = 0;
       // PlaerAC.SetTrigger("ExitCombo");
    }

    public void Attack_A()
    {

        if (Count == 0)
        {
            StopCoroutine("Cancel_Attack");
            StartCoroutine("Cancel_Attack");
            if (Now_State == null)
            {               
                Now_State = combo_State.ToString();
                Debug.Log(Now_State);

                PlaerAC.SetTrigger(Now_State);
            }
            else
            {
                Now_State += "A";
                Debug.Log(Now_State);


                PlaerAC.SetTrigger(Now_State);
                if (Now_State == "Attack_AAAA")
                {
                    Now_State = null;
                    StopCoroutine("Cancel_Attack");
                   
                }
                
            }
           

        }
       
       
    }
    public void Attack_B()
    {
       
        if (Now_State == null)
        {
            Now_State = "Attack_B";
            PlaerAC.SetTrigger(Now_State);

            Debug.Log(Now_State);
            Now_State = null;
        }
        else
        {
            Now_State += "B";
            Count += 1;
            
            Debug.Log(Now_State);
            StopCoroutine("Cancel_Attack");
            StartCoroutine("Cancel_Attack");

            PlaerAC.SetTrigger(Now_State);
            if (Count == 2 || Now_State == "Attack_AAAB")
            {
                Now_State = null;
                Count = 0;
                StopCoroutine("Cancel_Attack");

            }
            
        }
       
    }

}
