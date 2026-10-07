using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item",menuName ="New Item")]
public class Item : ScriptableObject
{
    [Header("Info")]
    public int ItemID;
    public string Name;
    public string Description;
    public int UnlockLev;
    public int MaxHP;
    public float MaxHPMul;
    public int MaxMP;
    public float MaxMPMul;
    public int MaxAtK;
    public float MaxAtkMul;
    public int MaxDef;
    public float MaxDefMul;
    public int Status;
}
