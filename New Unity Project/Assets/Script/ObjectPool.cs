using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool objectPool;
    public GameObject Attack_B;
    public GameObject Attack_ABB;
    public GameObject Attack_AAB;
    public GameObject Attack_AABB;
    public GameObject Attack_AAAB;

    public int PooledAmount=5;
    public int PooledAmount_Useless;


    public List<GameObject> Attack_Bs;
    public List<GameObject> Attack_ABBs;
    public List<GameObject> Attack_AABs;
    public List<GameObject> Attack_AABBs;
    public List<GameObject> Attack_AAABs;

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

        Attack_ABBs = new List<GameObject>();
        Attack_AABs = new List<GameObject>();
        Attack_AABBs = new List<GameObject>();
        Attack_AAABs = new List<GameObject>();
        for (int i = 0; i < PooledAmount_Useless; i++)
        {
            GameObject obj1 = (GameObject)Instantiate(Attack_ABB);
            obj1.SetActive(false);
            Attack_ABBs.Add(obj1);

            GameObject obj2 = (GameObject)Instantiate(Attack_AAB);
            obj2.SetActive(false);
            Attack_AABs.Add(obj2);

            GameObject obj3 = (GameObject)Instantiate(Attack_AABB);
            obj3.SetActive(false);
            Attack_AABBs.Add(obj3);

            GameObject obj4 = (GameObject)Instantiate(Attack_AAAB);
            obj4.SetActive(false);
            Attack_AAABs.Add(obj4);
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
    public GameObject GetAttack_ABB()
    {
        for (int i = 0; i < Attack_ABBs.Count; i++)
        {
            if (!Attack_ABBs[i].activeInHierarchy)
            {
                return Attack_ABBs[i];
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
    public GameObject GetAttack_AABB()
    {
        for (int i = 0; i < Attack_AABBs.Count; i++)
        {
            if (!Attack_AABBs[i].activeInHierarchy)
            {
                return Attack_AABBs[i];
            }
        }

        return null;
    }

    public GameObject GetAttack_AAAB()
    {
        for (int i = 0; i < Attack_AAABs.Count; i++)
        {
            if (!Attack_AAABs[i].activeInHierarchy)
            {
                return Attack_AAABs[i];
            }
        }

        return null;
    }
}
