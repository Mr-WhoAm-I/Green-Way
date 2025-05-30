using UnityEngine;

public abstract class MagicWeapon : Weapon
{
    [SerializeField] protected GameObject magicProjectilePrefab;  
    [SerializeField] protected float magicProjectileSpeed;

    public override void Attack()
    {
        CastSpell();
    }
    public abstract void CastSpell();
}
