
using UnityEngine;

public class Bruiq : MonoBehaviour
{
    private Player player;
    private HealthComponent healthComponent;
    protected void Awake()
    {
        player = GetComponentInParent<Player>();
        healthComponent = GetComponentInParent<HealthComponent>();
        player.SetSpeed(7f);
        healthComponent.SetMaxHealth(10);
    }
}
