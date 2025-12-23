using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Entity : MonoBehaviour
{

    protected Animator anim;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer sr;


    [Header("Health")]
    [SerializeField] private int maxHeath = 1;
    [SerializeField] private int currentHealth;
    [SerializeField] private Material DamageMaterial;
    [SerializeField] private float damageFeedbackDuration = .2f;
    private Coroutine damageFeedbackCoroutine; //store it here to avoide multiple damage taken at the same time

    [Header("Attack Details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask WhatIsTarget;


    
    //facing direction details
    protected int facingDir = 1;
    protected bool canMove = true;
    protected bool facingright = true;
    


    [Header("Collision Details")]
    [SerializeField] private float groundcheckDistance;
    protected bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;


    

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        currentHealth = maxHeath;
    }

    protected virtual void Update()
    {
        Handlemovement();
        HandleAnimations();
        Handleflip();
        HandleCollision();
    }


    public void DamageTargets()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, WhatIsTarget);

        foreach (Collider2D enemy in enemyColliders)
        {
            Entity entityTarget = enemy.GetComponent<Entity>();
            entityTarget.TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentHealth = currentHealth - 1;
        PlayDamageFeedback();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void PlayDamageFeedback()
    {
        if (damageFeedbackCoroutine != null)
        {
            StopCoroutine(damageFeedbackCoroutine);
        }
        StartCoroutine(DamageFeedbackCo());
    }

    private IEnumerator DamageFeedbackCo()
    {
        Material originalMat = sr.material;
        sr.material = DamageMaterial;
        yield return new WaitForSeconds(damageFeedbackDuration);
        sr.material = originalMat;
    }


    protected virtual void Die()
    {
        anim.enabled = false;
        col.enabled = false;

        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

        Destroy(gameObject, 3);
    }

    public virtual void EnableMovementAndJump(bool enable)//it is pubilc because we will be able to access it outside the script
    {
        canMove = enable;
        
    }


    protected void HandleAnimations()
    {

        anim.SetBool("isGrounded",isGrounded);

        anim.SetFloat("yVelocity",rb.linearVelocity.y);
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
    }

    

    protected virtual void Handlemovement()
    {
    }


    protected virtual void HandleAttack()
    {
        if (isGrounded)
        {
            anim.SetTrigger("attack");
        }
    }

    


    protected virtual void HandleCollision()//it shoots the ray in a particular direction
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundcheckDistance, whatIsGround);
    }


    protected virtual void Handleflip()
    {
        if(rb.linearVelocity.x > 0 && facingright == false)
        {
            flip();
        }else if(rb.linearVelocity.x<0 && facingright == true)
        {
            flip();
        }
    } 
    public void flip()
    {
        transform.Rotate(0, 180, 0);
        facingright = !facingright;
        facingDir = facingDir * -1;
    }


    private void OnDrawGizmos() //visually display info on screen in the from of line
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundcheckDistance));

        if(attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
 