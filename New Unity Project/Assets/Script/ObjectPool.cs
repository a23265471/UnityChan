using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool objectPool;
    public GameObject Attack_B;
    public int PooledAmount=5;


    public List<GameObject> Attack_Bs;
    // Use this for initialization

    private void Awake()
    {
        objectPool = this;
    }

    void Start ()
    {
        Attack_Bs = new List<GameObject>();
        for(int i = 0; i < PooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(Attack_B);
            obj.SetActive(false);
            Attack_Bs.Add(obj);

        }
		
	}
	
	public GameObject GetAttack_B()
    {
        for(int i = 0; i < Attack_Bs.Count; i++)
        {
            if (!Attack_Bs[i].activeInHierarchy)
            {
                return Attack_Bs[i];
            }
        }

        return null;
    }
}
