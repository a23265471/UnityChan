using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {

    public UnityChan_Move UnityChanMove;

    public Transform PlayerBack;
    public Transform target;
    public float smoothing = 5f;
    public float RotX;
    private Vector3 targetCamPos;
    private Vector3 offset;

    // Use this for initialization
    void Start () {

        offset = transform.position - target.position;
        RotX= transform.rotation.x;
        Debug.Log(RotX);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
       // Debug.Log(transform.rotation);
    }

    public void Player_Back()
    {
        Quaternion newRotation;
        
        newRotation = Quaternion.LookRotation(UnityChanMove.LookAtPoint);
        
        transform.position = PlayerBack.position;
        transform.rotation = newRotation;
        transform.rotation = Quaternion.Euler(31.892f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

      //  Debug.Log(transform.rotation.eulerAngles.x);
       // transform.rotation = Quaternion.Euler(31, transform.rotation.y, transform.rotation.z);

       
        offset = transform.position - target.position;



    }
}
