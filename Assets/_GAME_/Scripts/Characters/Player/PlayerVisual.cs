using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{

    public static PlayerVisual Instance { get; private set; }
    protected Animator animator;
    private SpriteRenderer spriteRenderer;
    private const string IS_RUNNING = "IsRunning";
    private const string IS_DIE = "IsDie";
    private const string TAKEHIT = "TakeHit";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        Player.Instance.OnTakeHit += Player_OnTakeHit;
        Player.Instance.OnDeath += Player_OnDeath;
    }

    private void Player_OnDeath(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_DIE, true);
    }

    private void Player_OnTakeHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger(TAKEHIT);
    }

    protected virtual void Update()
    {
        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
        AdjustPlayerFacingDirection();
    }

    private void AdjustPlayerFacingDirection()
    {


        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 PlayerPosition = Player.Instance.GetPlayerScreenPosition();
        spriteRenderer.flipX = mousePos.x < PlayerPosition.x;

    }


}
