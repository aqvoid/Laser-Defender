using System.Collections;
using UnityEngine.InputSystem;

public class PlayerAttack : Attack
{
    private PlayerInput playerInput;
    private InputAction attackAction;

    private bool isAttacking;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        attackAction = playerInput.actions["Attack"];
        attackAction.started += ctx => StartAttack();
        attackAction.canceled += ctx => StopAttack();
    }

    private void OnDisable()
    {
        attackAction.started -= ctx => StartAttack();
        attackAction.canceled -= ctx => StopAttack();
    }

    private IEnumerator AttackLoop()
    {
        while (isAttacking)
        {
            if (canAttack)
            {
                ShootProjectile(transform.up);
                StartCoroutine(AttackCooldown());
            }
            yield return null;
        }
    }

    protected override void StartAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(AttackLoop());
        }
    }

    protected override void StopAttack()
    {
        isAttacking = false;
    }

    
}
