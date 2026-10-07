using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject[] prefabs;
    private List<GameObject>[] pools;
    public int poolSize = 10; // 미리 생성할 개수

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(prefabs[i], transform);
                obj.SetActive(false);
                pools[i].Add(obj);
            }
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                
                if (select.CompareTag("Enemy"))
                {
                    EnemyController enemy = select.GetComponent<EnemyController>();
                    if (enemy != null)
                        enemy.Init();
                }
                
                return select;
                break;
            }
        }
        return null;
    }
}
