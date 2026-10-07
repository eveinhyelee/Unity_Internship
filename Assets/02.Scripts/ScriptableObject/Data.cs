using UnityEngine;

[CreateAssetMenu(fileName = "Data",menuName ="New Data")]
public class Data : ScriptableObject
{
    [Header("Info")]
    public string MonsterID;
    public string Name;
    public string Description;
    public int Attack;
    public float AttackMul;
    public int MaxHP;
    public float MaxMul;
    public int AttackRange;
    public float AttackRangeMul;
    public float AttackSpeed;
    public float MoveSpeed;
    public int MinExp;
    public int MaxExp;
    public int[] DropItem;

    public Sprite sprite;
}
