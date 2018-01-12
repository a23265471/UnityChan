using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiveFalse : MonoBehaviour {

    private void OnEnable()
    {
        Invoke("SetAtiveFalse",2);
    }
    void SetAtiveFalse()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}
