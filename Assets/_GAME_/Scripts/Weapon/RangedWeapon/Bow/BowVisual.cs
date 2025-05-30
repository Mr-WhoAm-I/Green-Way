using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowVisual : MonoBehaviour
{
    [SerializeField] private Bow bow;
    private const string ATTACK = "Attack";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        bow.OnWeaponSwing += Bow_OnBowFire;
    }

    private void Bow_OnBowFire(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }

    public void SpawnArrow()
    {
        bow.SpawnArrow();
    }
}
