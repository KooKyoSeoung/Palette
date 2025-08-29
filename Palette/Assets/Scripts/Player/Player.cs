using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected GameObject playerVFX;
    protected GameObject bulletPrefab;
    protected GameObject bulletPool;

    protected Rigidbody2D rigidbody;
    protected PlayerHealthUI healthUI;
    protected SpriteRenderer spriterenderer;
    protected int UIId;

    private KeyCode keyLeft = KeyCode.LeftArrow;
    private KeyCode keyRight = KeyCode.RightArrow;
    private KeyCode keyJump = KeyCode.Space;
    private KeyCode keyAtk = KeyCode.Z;
    protected KeyCode keySkill = KeyCode.X;

    protected float curTime = 0;
    protected float coolTime = 0.3f;

    protected float curSkillTime = 0;
    protected float coolSkillTime;

    protected int maxHealth = 3;
    protected int health = 3;
    protected float moveSpeed = 6.0f;
    protected float jumpPower = 15.0f;
    protected float horizontalMovement = 0.0f;

    protected bool isGrounded = false;
    protected bool isInvincibleTime = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    protected void PlayerMove()
    {
        horizontalMovement = 0.0f;

        if (Input.GetKey(keyLeft))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

            horizontalMovement = -1.0f;
        }
        if (Input.GetKey(keyRight))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

            horizontalMovement = 1.0f;
        }

        rigidbody.velocity = new Vector3(horizontalMovement * moveSpeed, rigidbody.velocity.y, 0.0f);
    }

    protected void PlayerJump()
    {
        if (Input.GetKeyDown(keyJump) && isGrounded)
        {
            Manager.Sound.PlaySFX("PlayerJump");

            isGrounded = false;

            rigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode2D.Impulse);
        }
    }

    protected void PlayerAttack()
    {
        if (curTime <= 0)
        {
            if (Input.GetKey(keyAtk))
            {
                GameObject bulletObject = Manager.Pool.Pop(bulletPrefab, bulletPool.transform).gameObject;
                bulletObject.transform.position = transform.position;
                bulletObject.transform.rotation = transform.rotation;
            }
            curTime = coolTime;
        }
        curTime -= Time.deltaTime;
    }

    protected virtual void PlayerSkill()
    {

    }

    protected void PlayerHeal(int value)
    {
        if (health + value > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += value;
        }
        healthUI.DrawUI(UIId, health);
    }

    protected void CheatHeal() // ġƮ
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            PlayerHeal(1);
    }

    public void OnDamaged()
    {
        if (isInvincibleTime)
            return;

        Manager.Sound.PlaySFX("PlayerHit");

        if (health > 1)
        {
            health -= 1;
            healthUI.DrawUI(UIId, health);
            StartCoroutine(Invincible(1.0f));
        }
        else
        {
            health -= 1;
            healthUI.DrawUI(UIId, health);
            OnPlayerDead();
        }
    }

    private void OnPlayerDead()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
        foreach (var bullet in bullets)
            Manager.Pool.Push(Utils.GetOrAddComponent<Poolable>(bullet));

        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private IEnumerator Invincible(float invincibleTime)
    {
        isInvincibleTime = true;
        StartCoroutine(BlinkEffect(invincibleTime, spriterenderer));
        yield return new WaitForSeconds(invincibleTime);
        spriterenderer.color = new Color(1, 1, 1, 1);
        isInvincibleTime = false;
    }

    private IEnumerator BlinkEffect(float blinkTime, SpriteRenderer spriteRenderer)
    {
        float remainingTime = 0.0f;
        float startTime = Time.time;

        while (remainingTime < blinkTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(0.15f);
            spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.15f);
            remainingTime = Time.time - startTime;
        }
    }
}
