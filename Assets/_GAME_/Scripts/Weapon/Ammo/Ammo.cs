using UnityEngine;

public abstract class Ammo : MonoBehaviour
{
    [SerializeField] protected int damageAmount;
    [SerializeField] protected float speed;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] private float lifetime = 5f;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        Destroy(gameObject, lifetime);
    }

    protected virtual void Update()
    {
        RotateTowardsMovement();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
        {
            enemyEntity.TakeDamage(damageAmount);
        }
        Destroy(gameObject);
    }

    protected virtual void RotateTowardsMovement()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public abstract void Initialize(Vector2 direction, int floor);

    public abstract void Move();
}