using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController player;
    public ObjectPool pool;
    
    private const int InitialStage = 1;

    private int level = 1;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    
    private int stage = 1;
    public int Stage
    {
        get { return stage; }
        set { stage = value; }
    }
    
    private int gameTime;
    private float timer;
    
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        timer += Time.deltaTime;
        stage = Mathf.FloorToInt(timer / 10f) + InitialStage;
    }
}
