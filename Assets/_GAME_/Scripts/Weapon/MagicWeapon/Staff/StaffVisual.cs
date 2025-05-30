using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffVisual : MonoBehaviour
{
    [SerializeField] private Staff staff;
    private const string ATTACK = "Attack";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        staff.OnWeaponSwing += Bow_OnStaffCastSpell;
    }

    private void Bow_OnStaffCastSpell(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }

    public void SpawnSpell()
    {
        staff.SpawnSpell();
    }
}
