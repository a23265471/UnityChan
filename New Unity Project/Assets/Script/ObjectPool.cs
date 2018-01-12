using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool objectPool;
    public GameObject Attack_B;
    public GameObject Attack_AAB;
    public int PooledAmount=5;


    public List<GameObject> Attack_Bs;
    public List<GameObject> Attack_AABs;

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
        Attack_AABs = new List<GameObject>();
        for (int i = 0; i < PooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(Attack_AAB);
            obj.SetActive(false);
            Attack_AABs.Add(obj);

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
    public GameObject GetAttack_AAB()
    {
        for (int i = 0; i < Attack_AABs.Count; i++)
        {
            if (!Attack_AABs[i].activeInHierarchy)
            {
                return Attack_AABs[i];
            }
        }

        return null;
    }

}
