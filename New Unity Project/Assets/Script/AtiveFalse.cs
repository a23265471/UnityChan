using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiveFalse : MonoBehaviour {

    


    private void OnEnable()
    {
        float destroyTime;

        if (Player_Attack.playerAttack.attackAAB||Player_Attack.playerAttack.attackABB)
        { destroyTime = 3; }
        else
        { destroyTime = 2; }

        Invoke("SetAtiveFalse", destroyTime);
    }
    void SetAtiveFalse()
    {
        Player_Attack.playerAttack.attackAAB = false;
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}
