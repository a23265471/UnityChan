using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChan_Move : MonoBehaviour {

    public joystick Joystick;
    private Vector3 movement;
    private Animator anim;
    private Rigidbody rigi;
    private CapsuleCollider PlayerCollider;
    public float Speed;
    public float JumpSpeed;
    private int Jump1;
    public float RotateSpeed;
    public float JumpHeight;
    public float PlayerGravity;
    bool IsGrounded=false;
    float distToGround;
    int FloorMask;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody>();
        PlayerCollider = GetComponent<CapsuleCollider>();
        FloorMask = LayerMask.GetMask("Floor");
        distToGround = PlayerCollider.bounds.extents.y;
        Physics.gravity = new Vector3(0,-(PlayerGravity), 0);
    }
        // Update is called once per frame
        void FixedUpdate () {

        float x = Joystick.Horizontal();
        float z = Joystick.Vertical();

        if (Physics.Raycast(transform.position, -Vector3.up, distToGround , FloorMask))
        {
            IsGrounded = true;
            Debug.Log("Grounded");

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

    private void move(float x,float z)
    {
        if (IsGrounded == false)
            Speed = JumpSpeed;
        movement.Set(x, 0, z);
        movement = movement * Speed * Time.deltaTime;
        rigi.MovePosition(transform.position + movement);
        
    }
    private void Turning()
    {
        Quaternion newRotation;
        Vector3 LookAtPoint;
        if (movement!=new Vector3(0, 0, 0))
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
          
        }


    }
}
