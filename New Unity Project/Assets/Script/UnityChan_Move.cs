using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChan_Move : MonoBehaviour
{

    public joystick Joystick;
    private Vector3 movement;
    private Rigidbody rigi;
    private CapsuleCollider PlayerCollider;
    private Animator PlayerAC;
    public float Speed;
    public float JumpSpeed;
    public float JumpHeight;
    public float DoubleJumpHeight;
    public float PlayerGravity;
    private int JumpCount;
    private bool DoubleJump;
    private bool IsJumping;

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
        DoubleJump = false;

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        float x = Joystick.Horizontal();
        float z = Joystick.Vertical();



        if (Physics.Raycast(transform.position, -Vector3.up, distToGround - 0.7f, FloorMask))
        {
            Debug.Log(distToGround);
            IsGrounded = true;
            JumpCount = 1;
            Debug.Log("Grounded");





            //-------------------------Animation------------------------




            if (Input.GetKeyUp(KeyCode.Space))
            {
                rigi.AddForce(0, JumpHeight, 0, ForceMode.Impulse);
            }
            Turning();
            move(x, z);

        }
        else
        {
            IsGrounded = false;



            move(x, z);
        }

    }

    private void move(float x, float z)
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



    }
    private void Turning()
    {
        Quaternion newRotation;
        Vector3 LookAtPoint;
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
            Debug.Log("Jump");
            rigi.AddForce(0, JumpHeight, 0, ForceMode.Impulse);

            DoubleJump = true;
            IsJumping = true;


        }
        else if ((!IsGrounded && JumpCount == 1))
        {


            Debug.Log("DoubleJump");
            rigi.useGravity = false;
            rigi.useGravity = true;
            rigi.AddForce(0, DoubleJumpHeight, 0, ForceMode.Impulse);
            Physics.gravity = new Vector3(0, -(PlayerGravity), 0);
            DoubleJump = false;
            IsJumping = true;
            JumpCount -= 1;

        }

    }

    private void Move_Animation()
    {
        if (IsGrounded)
        {
            PlayerAC.SetBool("Run", true);

        }

    }

}