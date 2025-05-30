using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class WizardVisual : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private EnemyEntity enemyEntity;

    private Animator animator;
    private const string ATTACK = "Attack";
    private const string TAKEHIT = "TakeHit";
    private const string IS_DIE = "IsDie";
    private const string IS_RUNNING = "IsRunning";
    private const string CHASING_SPEED_MULTIPLIER = "ChasingSpeedMultiplier";


    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        enemyAI.OnEnemyAttack += EnemyAI_OnEnemyAttack;
        enemyEntity.OnTakeHit += EnemyEntity_OnTakeHit;
        enemyEntity.OnDeath += EnemyEntity_OnDeath;
    }

    private void EnemyEntity_OnDeath(object sender, System.EventArgs e)
    {
        ChatManager.Instance.AddMaxMessage("W H Y ?");
        animator.SetBool(IS_DIE, true);
    }

    private void EnemyEntity_OnTakeHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger(TAKEHIT);
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, enemyAI.IsRunning);
        animator.SetFloat(CHASING_SPEED_MULTIPLIER, enemyAI.GetRoamingAnimationSpeed());
    }

    private void OnDestroy()
    {
        enemyAI.OnEnemyAttack -= EnemyAI_OnEnemyAttack;
    }



    public void TriggerAttackAnimationTurnOff()
    {
        enemyEntity.PolygonColliderTurnOff();
    }

    public void TriggerAttackAnimationTurnOn()
    {
        enemyEntity.PolygonColliderTurnOn();
    }

    private void EnemyAI_OnEnemyAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }

}
