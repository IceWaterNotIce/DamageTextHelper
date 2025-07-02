using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DamageTextPool : MonoBehaviour
{
    public static DamageTextPool Instance;
    public GameObject damageTextPrefab;
    public int initialPoolSize = 30;
    
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewTextObject();
        }
    }

    GameObject CreateNewTextObject()
    {
        GameObject obj = Instantiate(damageTextPrefab, transform);
        obj.SetActive(false);
        pool.Enqueue(obj);
        return obj;
    }

    public GameObject GetDamageText()
    {
        if (pool.Count == 0)
        {
            CreateNewTextObject();
        }
        
        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(this.transform); // 可選：將物件放回池的父物件
        pool.Enqueue(obj);
    }
}