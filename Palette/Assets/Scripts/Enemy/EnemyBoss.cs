using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] private float attackInterval = 3.0f;
    [SerializeField] private float stunDuration = 2.0f;

    public BossState currentState = BossState.Idle;

    private Coroutine attackRoutine;

    private const float MAX_HEALTH = 100.0f;
    private float health;

    
    void Start()
    {
        health = MAX_HEALTH;
        ChangeState(BossState.Idle);
    }

    private void ChangeState(BossState newState)
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }

        currentState = newState;

        switch (newState)
        {
            case BossState.Idle:
                attackRoutine = StartCoroutine(AttackLoop());
                break;
            case BossState.Attack:
                break;
            case BossState.Stunned:
                StartCoroutine(StunRoutine());
                break;
            case BossState.Dead:
                Debug.Log("Dead");
                break;
        }
    }

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);

            if (currentState == BossState.Idle)
                ExecuteRandomAttack();
        }
    }

    private void ExecuteRandomAttack()
    {
        int pattern = Random.Range(0, 2); // 0~1 2개
        switch (pattern)
        {
            case 0:
                // 패턴 1
                break;
            case 1:
                // 패턴 2
                break;
        }
    }

    private IEnumerator StunRoutine()
    {
        Debug.Log("Boss Stun!!");
        yield return new WaitForSeconds(stunDuration);
        ChangeState(BossState.Idle);
    }

    public void ApplyStun()
    {
        if (currentState != BossState.Dead)
        {
            ChangeState(BossState.Stunned);
        }
    }

    public void OnDamage(float damage)
    {
        if (currentState == BossState.Dead) 
            return;

        health -= damage;
        // 여기에 HP UI 연동한거 작성

        if (health <= 0)
        {
            health = 0;
            ChangeState(BossState.Dead);
            return;
        }
    }
}

public enum BossState
{
    Idle,
    Attack,
    Stunned,
    Dead
}