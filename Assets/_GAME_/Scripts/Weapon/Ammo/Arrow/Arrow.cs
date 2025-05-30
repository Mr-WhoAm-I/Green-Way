using UnityEngine;
using UnityEngine.SceneManagement;

public class Arrow : Ammo
{
    [SerializeField] private string greenWaySortingLayer = "Player";
    [SerializeField] private int orderInLayer = 2;
    private SpriteRenderer visualRenderer;
    [SerializeField] private float minScale = 0.65f; // Минимальный масштаб
    [SerializeField] private float scaleSpeed = 1.5f;
    private float scaleTime;

    protected override void Awake()
    {
        base.Awake();
        damageAmount = 3;
        speed = 15f;
        visualRenderer = GetComponentInChildren<SpriteRenderer>();

        scaleTime += Time.deltaTime * scaleSpeed;
        float scale = Mathf.Lerp(1f, minScale, scaleTime);
        transform.localScale = new Vector3(scale, scale, 1f);
    }

    public override void Move()
    {
        rb.velocity = transform.right * speed;
    }

    public override void Initialize(Vector2 direction, int floor)
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "GreenWay" || floor == 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Weapon");
            if (visualRenderer != null)
            {
                visualRenderer.sortingLayerName = greenWaySortingLayer; // Player
                visualRenderer.sortingOrder = orderInLayer; // 2
            }
            Debug.Log("Arrow initialized for GreenWay: Layer Weapon, Sorting Layer: Player, Order: 2");
        }
        else if (currentScene == "SCDemo")
        {
            if (floor < 1 || floor > 3)
            {
                Debug.LogWarning($"Invalid floor {floor}, defaulting to 1");
                floor = 1;
            }
            string weaponLayer = $"Weapon_Layer{floor}";
            string sortingLayer = $"Layer {floor}";
            gameObject.layer = LayerMask.NameToLayer(weaponLayer);
            if (visualRenderer != null)
            {
                visualRenderer.sortingLayerName = sortingLayer; // Layer 1, Layer 2, Layer 3
                visualRenderer.sortingOrder = orderInLayer; // 2
            }
            Debug.Log($"Arrow initialized: Floor {floor}, Layer {weaponLayer}, Sorting Layer: {sortingLayer}, Order: {orderInLayer}");
        }

        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Move();
    }
}