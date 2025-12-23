using UnityEngine;

public class Player : Entity
{
    [Header("Movement Details")]
    [SerializeField] protected float movespeed = 4.1f;
    [SerializeField] private float jumpForce = 15;

    private float xinput;
    private bool canJump = true;


    protected override void Update()
    {
        base.Update();
        Handleinput();
    }


    private void Handleinput()
    {
        xinput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Trytojump();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandleAttack();
        }
    }
    protected override void Handlemovement()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(xinput * movespeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); //character stop while attacking
        }
    }


    private void Trytojump()
    {
        if (isGrounded && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }


    public override void EnableMovementAndJump(bool enable)
    {
        base.EnableMovementAndJump(enable);
        canJump = enable;
    }


    protected override void Die()
    {
        base.Die();
        UIClass.instance.EnableGameOverUI();
    }
}
