using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizA : MonoBehaviour
{
    private Player player;
    private HealthComponent healthComponent;
    protected void Awake()
    {
        player = GetComponentInParent<Player>();
        healthComponent = GetComponentInParent<HealthComponent>();
        player.SetSpeed(5f);
        healthComponent.SetMaxHealth(30);
    }
}
