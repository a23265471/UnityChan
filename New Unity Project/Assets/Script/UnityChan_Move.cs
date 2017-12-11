using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChan_Move : MonoBehaviour {

    public joystick Joystick;
    private Vector3 movement;
    private Animator anim;
    private Rigidbody rigi;
    public float Speed;
    public float RotateSpeed;
    public float Jump;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate () {

        float x = Joystick.Horizontal();
        float z = Joystick.Vertical();
       

        move(x, z);
    }

    private void move(float x,float z)
    {
        movement.Set(x, 0, z);
        movement = movement * Speed * Time.deltaTime;
        rigi.MovePosition(transform.position + movement);
       
    }
}
