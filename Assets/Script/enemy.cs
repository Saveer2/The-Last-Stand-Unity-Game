using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class enemy : Entity
{

    private bool playerDetected;

    [Header("Movement Details")]
    [SerializeField] protected float movespeed = 4.1f;

    protected override void Update()    {
        base.Update();
        HandleAttack();
    }


    protected override void HandleAttack()
    {
        if (playerDetected)
        {
            anim.SetTrigger("attack");
        }
    }

    protected override void Handlemovement()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(facingDir * movespeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); //character stop while attacking
        }
    }


    protected override void HandleCollision()
    {
        base.HandleCollision();
        playerDetected = Physics2D.OverlapCircle(attackPoint.position, attackRadius, WhatIsTarget);
    }
}
