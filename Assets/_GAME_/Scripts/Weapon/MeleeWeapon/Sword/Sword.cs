public abstract class Sword : MeleeWeapon
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Attack()
    {
        AttackCollider();
        TriggerWeaponSwingEvent();
    }
}
