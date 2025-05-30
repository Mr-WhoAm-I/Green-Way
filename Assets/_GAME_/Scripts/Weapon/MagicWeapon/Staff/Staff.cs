using UnityEngine;

public class Staff : MagicWeapon
{
    private bool isAttacking;
    protected override void Awake()
    {
        base.Awake();
        damageAmount = 5;
    }

    public override void CastSpell()
    {
        if (isAttacking) return;

        isAttacking = true;
        TriggerWeaponSwingEvent();  
    }

    public void SpawnSpell()
    {
        GameObject fireball = Instantiate(magicProjectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * magicProjectileSpeed;

        isAttacking = false;
    }
}
