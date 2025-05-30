using UnityEngine;

public class Bow : RangedWeapon
{
    private bool isAttacking;

    protected override void Awake()
    {
        base.Awake();
        damageAmount = 3;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public override void Fire()
    {
        if (isAttacking) return;

        isAttacking = true;
        TriggerWeaponSwingEvent();
    }

    public void SpawnArrow()
    {
        Vector2 direction = transform.right;
        int floor = GetParentFloor();

        GameObject arrowObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Arrow arrow = arrowObj.GetComponent<Arrow>();
        if (arrow != null)
        {
            arrow.Initialize(direction, floor);
        }
        else
        {
            Debug.LogWarning("Arrow component not found, using fallback velocity");
            Rigidbody2D rb = arrowObj.GetComponent<Rigidbody2D>();
            rb.velocity = direction * projectileSpeed;
        }

        isAttacking = false;
    }

    private int GetParentFloor()
    {
        Transform parent = transform.parent;
        if (parent == null)
        {
            Debug.LogWarning("No parent found for Bow, defaulting to floor 1");
            return 1;
        }

        string parentLayer = LayerMask.LayerToName(parent.gameObject.layer);
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (currentScene == "GreenWay")
        {
            return 0; 
        }

        switch (parentLayer)
        {
            case "Layer 1":
                return 1;
            case "Layer 2":
                return 2;
            case "Layer 3":
                return 3;
            default:
                Debug.LogWarning($"Unknown parent layer {parentLayer}, defaulting to floor 1");
                return 1;
        }
    }
}