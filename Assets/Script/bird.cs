using UnityEngine;

public class bird : Entity
{
    [Header("Extra details")]
    [SerializeField] private Transform player;

    protected override void Awake()
    {
        base.Awake();
        player = FindFirstObjectByType<Player>().transform;
    }
    protected override void Update()
    {
        Handleflip();
    }

    protected override void Handleflip()
    {

        if (player != null)
        {
            return;
        }
        if (player.transform.position.x > transform.position.x && facingright == true)
        {
            flip();
        }
        else if (player.transform.position.x < transform.position.x && facingright == false)
        {
            flip();
        }
    }


    protected override void Die()
    {
        base.Die();
        UIClass.instance.EnableGameOverUI();
    }
}
