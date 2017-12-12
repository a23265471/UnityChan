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
        distToGround = PlayerCollider.bounds.extents.y;
        FloorMask= LayerMask.GetMask("Floor");
        Physics.gravity = new Vector3(0,-(PlayerGravity), 0);
    }
        // Update is called once per frame
        void FixedUpdate () {

        float x = Joystick.Horizontal();
        float z = Joystick.Vertical();
       
        if(Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, FloorMask))
        {
            IsGrounded = true;
            Debug.Log("Grounded");

            if (Input.GetKeyUp(KeyCode.Space))
            {
                rigi.AddForce(0, JumpHeight, 0, ForceMode.Impulse);
            }

        }
        else IsGrounded = false;



        move(x, z);
        
    }

    private void move(float x,float z)
    {
        movement.Set(x, 0, z);
        movement = movement * Speed * Time.deltaTime;
        rigi.MovePosition(transform.position + movement);
        
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
