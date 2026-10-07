using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer characterRenderer;
    protected AnimationHandler animationHandler;
    public Data data;

    public int curHP;
    public int curEXP;
    public int maxEXP;
    public int maxHP;
    
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection
    {
        get { return movementDirection; }
    }
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection
    {
        get { return lookDirection; }
    }
    
    
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        characterRenderer = GetComponent<SpriteRenderer>();
        animationHandler = GetComponent<AnimationHandler>();
        
        curHP = data.MaxHP;
        maxHP = data.MaxHP;
        curEXP = 0;
        maxEXP = data.MaxExp;
    }
    protected virtual void Start()
    {
        
    }
    protected virtual void Update()
    {
        Rotate(lookDirection);
    }
    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
    }

    private void Movement(Vector2 direction)
    {
        direction *= data.MoveSpeed;
        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotz = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotz) > 90f;

        characterRenderer.flipX = isLeft;
    }
    
}
