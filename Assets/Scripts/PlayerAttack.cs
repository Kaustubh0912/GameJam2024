using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private Transform firePoint;
    private PlayerController playerController;
    private Animator anim;
    private float cooldownTimer=Mathf.Infinity;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer>attackCooldown && playerController.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
        cooldownTimer = 0;

        fireballs[0].transform.position = firePoint.position;
        fireballs[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
}
