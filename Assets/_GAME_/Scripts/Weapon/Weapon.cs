using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int damageAmount;

    public event EventHandler OnWeaponSwing;

    protected new PolygonCollider2D collider2D;

    protected virtual void Awake()
    {
        collider2D = GetComponent<PolygonCollider2D>();
    }

    public abstract void Attack();

    public void AttackCollider()
    {
        collider2D.enabled = !collider2D.enabled;
    }

    protected void TriggerWeaponSwingEvent()
    {
        OnWeaponSwing?.Invoke(this, System.EventArgs.Empty);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
        {
            enemyEntity.TakeDamage(damageAmount);
        }
    }
}
