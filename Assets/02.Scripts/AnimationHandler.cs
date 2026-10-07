using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsHitting = Animator.StringToHash("IsHit");
    private static readonly int IsDead= Animator.StringToHash("IsDead");

    protected Animator animator;
    
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > 0.5f);
    }
    public void Hit()
    {
        animator.SetBool(IsHitting,true);
        StartCoroutine(HitRoutine());
    }
    IEnumerator HitRoutine()
    {
        animator.SetBool(IsHitting, true);
        yield return new WaitForSeconds(0.3f);
        animator.SetBool(IsHitting, false);
    }

    public void Dead()
    {
        animator.SetTrigger(IsDead);
    }
}
