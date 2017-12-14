using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class joystick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler {

    private Image BGImage;
    private Image Joystick;
    public Vector3 InputVector;


	// Use this for initialization
	void Start () {

        BGImage = GetComponent<Image>();
        Joystick = transform.GetChild(0).GetComponent<Image>();
	}
	public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);

    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputVector = Vector3.zero;
        Joystick.rectTransform.anchoredPosition = InputVector;

    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(BGImage.rectTransform,
                                                                   ped.position,
                                                                   ped.pressEventCamera,
                                                                   out pos)) 
        {
            pos.x = (pos.x / BGImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / BGImage.rectTransform.sizeDelta.y);

            InputVector = new Vector3(pos.x * 2 - 1, 0, pos.y * 2 - 1);
            InputVector = (InputVector.magnitude > 1.0f) ? InputVector.normalized : InputVector;

            Debug.Log(InputVector);

            Joystick.rectTransform.anchoredPosition = new Vector3(InputVector.x * (BGImage.rectTransform.sizeDelta.x / 3)
                                                                , InputVector.z * (BGImage.rectTransform.sizeDelta.y / 3));

        }

    }

    public float Horizontal()
    {
        if (InputVector.x != 0)
            return InputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (InputVector.z != 0)
            return InputVector.z;
        else
            return Input.GetAxis("Vertical");
    }

    
}
