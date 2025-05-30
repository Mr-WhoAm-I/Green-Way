using System;
using UnityEngine;
using System.Collections;

[SelectionBase]
public class Player : Character
{
    public static Player Instance { get; private set; }

    private CapsuleCollider2D capsuleCollider2D;
    private BoxCollider2D boxCollider2D;

    protected float minMovingSpeed = 0.1f;
    private bool isRunning = false;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected override void Start()
    {
        base.Start();
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
    }

    private void GameInput_OnPlayerAttack(object sender, EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    protected override void Update()
    {
        base.Update();
        inputVector = GameInput.Instance.GetMovementVector();
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneTransitionManager.Instance.LoadScene(SceneTransitionManager.SceneName.LVL2, savePlayerData: true);
        }
    }

    protected override void HandleMovement()
    {
        base.HandleMovement();
        isRunning = Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed;
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public Vector3 GetPlayerScreenPosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    protected override void OnDeathHandler()
    {
        base.OnDeathHandler();
        boxCollider2D.enabled = false;
        capsuleCollider2D.enabled = false;

        SceneTransitionManager.Instance.LoadMenuWithDelay(4f);
    }

    public void SetSpeed(float newSpeed)
    {
        movingSpeed = newSpeed;
    }
}