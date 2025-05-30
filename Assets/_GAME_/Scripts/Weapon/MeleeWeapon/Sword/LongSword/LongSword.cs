public class LongSword : Sword
{
    protected override void Awake()
    {
        base.Awake();
        damageAmount = 5;  
    }

    public override void Attack()
    {
        base.Attack();
    }
}

