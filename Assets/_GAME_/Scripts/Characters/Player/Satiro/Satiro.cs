using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satiro : MonoBehaviour
{
    private Player player;
    private HealthComponent healthComponent;
    protected void Awake()
    {
        player = GetComponentInParent<Player>();
        healthComponent = GetComponentInParent<HealthComponent>();
        player.SetSpeed(3f);
        healthComponent.SetMaxHealth(20);
    }
}
