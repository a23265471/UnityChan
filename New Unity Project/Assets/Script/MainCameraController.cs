using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {

    public UnityChan_Move UnityChanMove;

    public Transform PlayerBack;
    public Transform target;
    public float smoothing = 5f;
    public Vector3 targetCamPos;
    private Vector3 offset;

    // Use this for initialization
    void Start () {

        offset = transform.position - target.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    public void Player_Back()
    {

       
        transform.Rotate(UnityChanMove.LookAtPoint);
    }
}
