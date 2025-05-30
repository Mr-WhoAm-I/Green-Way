using UnityEngine;

public class SwordVisual : MonoBehaviour
{

    [SerializeField] private Sword sword;

    private const string ATTACK = "Attack";
    private const string IsAttack = "IsAttack";
    private const string ISDIE = "IsDie";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        sword.OnWeaponSwing += Sword_OnSwordSwing;
        Player.Instance.OnDeath += Sword_OnDeath;
    }


    private void Sword_OnDeath(object sender, System.EventArgs e)
    {
        animator.SetBool(ISDIE, true);


    }
    #region SetSwordEntryAnimation
    public void AttackIsFalse()
    {
        animator.SetBool(IsAttack, false);
    }

    public void AttackIsTrue()
    {
        animator.SetBool(IsAttack, true);
    }
    #endregion
    private void Sword_OnSwordSwing(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }

    public void TriggerEndAttackAnimation()
    {
        sword.AttackCollider();
    }
}
