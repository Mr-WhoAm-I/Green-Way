using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;

    protected Rigidbody2D rb;
    protected HealthComponent healthComponent;
    protected HealthBar healthBar;

    protected bool isDead = false;
    protected Vector2 inputVector;
    protected float movingSpeed = 5f;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthComponent = GetComponentInParent<HealthComponent>();

        if (healthComponent == null)
        {
            Debug.LogError("HealthComponent не найден на объекте Character!");
        }
        healthComponent.LoadHealth();

    }

    protected virtual void Start()
    {
        healthBar = HealthBar.Instance;
        if (healthBar == null)
        {
            Debug.LogError("HealthBar не найден!");
            return;
        }

        healthComponent.OnHealthChanged += OnHealthChanged;
        healthComponent.OnDeath += OnDeathHandler;

        healthBar?.SetMaxHealth(healthComponent.GetCurrentHealth());
    }

    protected virtual void Update()
    {
        HandleInput();
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
    }

    protected virtual void HandleInput()
    {
    }


    protected virtual void HandleMovement()
    {
        if (isDead) return;
        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));
    }

    protected virtual void OnHealthChanged(int currentHealth)
    {
        healthBar?.SetHealth(currentHealth);
    }

    protected virtual void OnDeathHandler()
    {
        isDead = true;
        OnDeath?.Invoke(this, EventArgs.Empty);
    }

    public void TakeDamage(int damage)
    {
        healthComponent.TakeDamage(damage);
        OnTakeHit?.Invoke(this, EventArgs.Empty);
    }
}
