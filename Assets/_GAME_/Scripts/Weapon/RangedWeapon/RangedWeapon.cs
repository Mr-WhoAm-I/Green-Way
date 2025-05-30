using UnityEngine;

public abstract class RangedWeapon : Weapon
{
    [SerializeField] protected GameObject projectilePrefab;  
    [SerializeField] protected float projectileSpeed;

    public override void Attack()
    {
        Fire();
    }
    public abstract void Fire();
}
