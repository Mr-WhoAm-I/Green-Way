using System;
using UnityEngine;

public class BruigVisual : PlayerVisual
{
    private const string ATTACK = "Attack";

    protected override void Start()
    {
        base.Start();  
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
    }

    private void GameInput_OnPlayerAttack(object sender, EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }

    protected override void Update()
    {
        base.Update();  
    }
}
