using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private Camera camera;
    public Action<int> OnHPChanged; // 동기화해주기 위한 액션이벤트
    public Action<int> OnEXPChanged; // 동기화해주기 위한 액션이벤트
    [SerializeField] private GameObject DeadCanvas;
    
    void Update()
    {
        if (curHP == 0)
        {
            OnDaed();
        }
    }

    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }

    void OnLook(InputValue inputValue)
    {
        camera = Camera.main;
        
        Vector2 mousePosition = inputValue.Get<Vector2>();
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < 0.9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Item")) return;
        
        DropItem drop = collision.GetComponent<DropItem>();
        if (drop != null && drop.itemData != null)
        {
            Heal(drop.itemData.MaxHP); 
            Destroy(collision.gameObject); 
        }
    }

    public void Heal(int amount)
    {
        curHP += amount;
        if (curHP > data.MaxHP)
            curHP = data.MaxHP;
        OnHPChanged?.Invoke(curHP);
    }
    public void TakeDamage(int amount)
    {
        curHP -= amount;
        if (curHP < 0)
            curHP = 0;
        OnHPChanged?.Invoke(curHP);
    }
    public void GetExp(int amount)
    {
        curEXP += amount;
        if (curEXP >= maxEXP)
        {
            curEXP = 0;
            GameManager.instance.Level++;
        }
        OnEXPChanged?.Invoke(curEXP);
    }

    void OnDaed()
    {
        animationHandler.Dead();
        DeadCanvas.SetActive(true);
    }
}
