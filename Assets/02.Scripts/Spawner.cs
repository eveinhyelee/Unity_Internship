using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private int previousStage;
    private int curStage;
    public Transform[] spawnPoint;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        previousStage = GameManager.instance.Stage;
        Spawn();
    }

    void Update()
    {
        curStage = GameManager.instance.Stage;

        if (curStage > previousStage) //스테이지가 변경될때마다
        {
            Spawn();
            previousStage = curStage;
        }
    }

    void Spawn()
    {
        int spawnCount = Random.Range(1, 6);
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject enemy = GameManager.instance.pool.Get(0);
            
            if (curStage >= 5)
            {
                enemy = GameManager.instance.pool.Get(Random.Range(0, 2));
            }
            int randIndex = Random.Range(0, spawnPoint.Length);
            enemy.transform.position = spawnPoint[randIndex].position;
        }
    }
}