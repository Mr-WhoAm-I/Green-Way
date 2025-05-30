using System.Collections;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    public static TextScript Instanse { get; private set; }

    [SerializeField] private EnemyEntity enemyEntity;

    private void Awake()
    {
        Instanse = this;
    }

    private void Start()
    {
        Player.Instance.OnDeath += Player_OnDeath;
        enemyEntity.OnDeath += EnemyEntity_OnDeath;
    }

    private void EnemyEntity_OnDeath(object sender, System.EventArgs e)
    {
        ChatManager.Instance.AddMinMessage("W H Y   ?");
    }

    private void Player_OnDeath(object sender, System.EventArgs e)
    {
        ChatManager.Instance.AddMaxMessage("You DIED, RIP");
    }
}
