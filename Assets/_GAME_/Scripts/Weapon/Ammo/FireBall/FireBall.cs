using UnityEngine;

public class Fireball : Ammo
{
    protected override void Awake()
    {
        base.Awake();
        damageAmount = 5;  
        speed = 4f;  
    }

    public override void Move()
    {
        rb.velocity = transform.right * speed;  
    }

    public override void Initialize(Vector2 direction, int floor)
    {
        throw new System.NotImplementedException();
    }
}
