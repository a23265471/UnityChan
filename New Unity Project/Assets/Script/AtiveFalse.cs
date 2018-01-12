using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiveFalse : MonoBehaviour {
    
    private void OnEnable()
    {
        
        float destroyTime;
        
        destroyTime = 2f;

        Invoke("SetAtiveFalse", destroyTime);
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
