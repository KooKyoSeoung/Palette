using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] private float attackInterval = 5.0f;
    [SerializeField] private float stunDuration = 5.0f;
    [SerializeField] private Slider healthUI;
    [SerializeField] private GameObject weakPoint;
    [SerializeField] private MainScene mainScene;

    [Header ("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 8.0f;
    [SerializeField] private Transform firePoint;

    private Player player;
    private Transform playerTransform;

    private SpriteRenderer spriterenderer;
    private Color originColor = new Color(1, 1, 1, 1);
    private Color stunColor = new Color(0.5f, 0.5f, 0.5f, 1);

    private BossState currentState = BossState.Idle;

    private Coroutine attackRoutine;

    private const float MAX_HEALTH = 100.0f;
    private float health;
    private bool isNeedInit = true;
    
    void Start()
    {
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        weakPoint.SetActive(false);
        health = MAX_HEALTH;
        healthUI.value = MAX_HEALTH;
        //ChangeState(BossState.Idle);
    }

    void Update()
    {
        if (isNeedInit && Manager.Instance.player != null)
        {
            isNeedInit = false;

            player = Manager.Instance.player.GetComponent<Player>();
            playerTransform = Manager.Instance.player.GetComponent<Transform>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.OnDamaged();
        }
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
                mainScene.BossDeadCall();
                healthUI.gameObject.SetActive(false);
                gameObject.SetActive(false);
                break;
            case BossState.Stoped:
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
        int pattern = Random.Range(0, 2); // 0~1 2°³
        switch (pattern)
        {
            case 0:
                StartCoroutine(ShootThreeShot());
                break;
            case 1:
                StartCoroutine(ShootSpreadShot());
                break;
        }
    }

    private IEnumerator ShootThreeShot()
    {
        if (player == null) yield break;

        transform.DOShakePosition(1f);
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < 3; i++)
        {
            ShootAtPlayer();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void ShootAtPlayer()
    {
        Vector2 direction = (playerTransform.position - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    private IEnumerator ShootSpreadShot()
    {
        if (player == null) yield break;

        transform.DOShakePosition(1.5f);
        yield return new WaitForSeconds(2f);

        Vector2 playerPos = playerTransform.position;
        Vector2 direction = (playerPos - (Vector2)transform.position).normalized;

        Vector2 perpendicular = new Vector2(-direction.y, direction.x);

        for (int i = -1; i <= 1; i++)
        {
            Vector2 modifiedDirection = (direction + perpendicular * 0.2f * i).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = modifiedDirection * bulletSpeed;
        }

        yield return null;
    }

    private IEnumerator StunRoutine()
    {
        spriterenderer.color = stunColor;
        yield return new WaitForSeconds(stunDuration);
        spriterenderer.color = originColor;
        ChangeState(BossState.Idle);
    }

    public void ApplyStun()
    {
        if (currentState != BossState.Dead)
        {
            ChangeState(BossState.Stunned);
        }
    }

    public void ChangeToStoped()
    {
        ChangeState(BossState.Stoped);
    }

    public void ChangeToIdle()
    {
        ChangeState(BossState.Idle);
    }

    public void OnDamage(float damage)
    {
        if (currentState == BossState.Dead) 
            return;

        health -= damage;
        healthUI.value = health;

        if (health <= 0)
        {
            health = 0;
            ChangeState(BossState.Dead);
            return;
        }
    }

    public void ShowWeakPoint()
    {
        if (weakPoint.activeSelf == false)
            StartCoroutine(ShowWeakPointCoroutine());
    }

    private IEnumerator ShowWeakPointCoroutine(float time = 7.0f)
    {
        weakPoint.SetActive(true);
        yield return new WaitForSeconds(time);
        weakPoint.SetActive(false);
    }
}

public enum BossState
{
    Idle,
    Attack,
    Stunned,
    Dead,
    Stoped
}