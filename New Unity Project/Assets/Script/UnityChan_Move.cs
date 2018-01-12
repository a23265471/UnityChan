using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChan_Move : MonoBehaviour
{
    public Player_Attack Player_attack;
    public joystick Joystick;
    public PlayerHealth playerHealth;
    private Vector3 movement;
    private Rigidbody rigi;
    private CapsuleCollider PlayerCollider;
    private Animator PlayerAC;
    public Vector3 LookAtPoint;
    private Quaternion FixedRotation;
    
    public float Speed;
    public float JumpSpeed;
    public float JumpHeight;
    public float DoubleJumpHeight;
    public float PlayerGravity;
    private int JumpCount;
    public bool IsJumping;
    

    bool IsGrounded = false;
    float distToGround;
    int FloorMask;

    private void Start()
    {
        rigi = GetComponent<Rigidbody>();
        PlayerCollider = GetComponent<CapsuleCollider>();
        PlayerAC = GetComponent<Animator>();
        FloorMask = LayerMask.GetMask("Floor");
        distToGround = PlayerCollider.bounds.extents.y;
        Physics.gravity = new Vector3(0, -(PlayerGravity), 0);
        FixedRotation = transform.rotation;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
       /* GetComponent<CapsuleCollider>().height = PlayerAC.GetFloat("colli_Height");
        GetComponent<CapsuleCollider>().center = new Vector3(0, PlayerAC.GetFloat("colli_pos_y"), PlayerAC.GetFloat("colli_pos_z"));*/

        float x = Joystick.Horizontal();
        float z = Joystick.Vertical();

       
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround - 0.7f, FloorMask))
        {
          //  Debug.Log(distToGround);
            IsGrounded = true;
            JumpCount = 1;
            IsJumping = false;

          //  Debug.Log("Grounded");
            move(x, z);
            Turning();

           
            PlayerAC.SetBool("DoubleJump", false);
        }
        else
        {
            IsGrounded = false;            
            move(x, z);
            Turning();
        }

        if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
        

    }

    private void move(float x, float z)
    {
        if (!Player_attack.IsAttack&&!playerHealth.isDead)
        {
           
            if (!IsGrounded)
            {
                movement.Set(x, 0, z);
                movement = movement * JumpSpeed * Time.deltaTime;
                rigi.MovePosition(transform.position + movement);

            }
            else
            {
                movement.Set(x, 0, z);
                movement = movement * Speed * Time.deltaTime;
                rigi.MovePosition(transform.position + movement);
            }

            //-----------------Animation----------------------
            Move_Animation();
        }
       
        

    }
    private void Turning()
    {
        Quaternion newRotation;
        Quaternion NowRotation;

        if (movement != new Vector3(0, 0, 0))
        {
            LookAtPoint = movement;
            newRotation = Quaternion.LookRotation(LookAtPoint);
            


            rigi.MoveRotation(newRotation);
        }
    }

   

    public void Jump()
    {
        if (IsGrounded)
        {
            //Debug.Log("Jump");
            rigi.AddForce(0, JumpHeight, 0, ForceMode.Impulse);
            IsJumping = true;

            //-------------Animation-----------------
            PlayerAC.SetTrigger("Jump");
        }
        else if ((!IsGrounded && JumpCount == 1))
        {
           // Debug.Log("DoubleJump");
            rigi.useGravity = false;
            rigi.useGravity = true;
            rigi.AddForce(0, DoubleJumpHeight, 0, ForceMode.Impulse);
            Physics.gravity = new Vector3(0, -(PlayerGravity), 0);
            JumpCount -= 1;
            IsJumping = true;

            //-------------Animation-----------------
           
            PlayerAC.SetBool("DoubleJump", true);

        }

    }

    private void Move_Animation()
    {
        if (IsGrounded && Joystick.InputVector!=new Vector3(0,0,0))
        {
            PlayerAC.SetBool("Idle", false);

            if(Joystick.InputVector.x>-0.5f && Joystick.InputVector.x < 0.5f && Joystick.InputVector.z > -0.5f && Joystick.InputVector.z < 0.5f)
            {
                PlayerAC.SetBool("Run", false);
                PlayerAC.SetBool("Walk", true);
            }
            else
            {
                PlayerAC.SetBool("Walk", false);
                PlayerAC.SetBool("Run", true);
            }
           

        }
        else if(!IsGrounded && Joystick.InputVector != new Vector3(0, 0, 0))
        {
            PlayerAC.SetBool("Walk", false);
            PlayerAC.SetBool("Run", false);
            PlayerAC.SetBool("Idle", false);
            //Jump
        }
        else
        {
            PlayerAC.SetBool("Walk", false);
            PlayerAC.SetBool("Run", false);
            PlayerAC.SetBool("Idle", true);
        }
        if (IsJumping)
        {
            PlayerAC.SetBool("Run", false);
            PlayerAC.SetBool("Idle", false);
        }

    }

    

   
}