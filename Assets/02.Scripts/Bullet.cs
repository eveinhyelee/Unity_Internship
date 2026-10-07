using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    public Item item;

    public void Init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;
    }
}