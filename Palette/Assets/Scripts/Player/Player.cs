using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected GameObject bulletPrefab;
    protected GameObject bulletPool;

    protected Rigidbody2D rigidbody;
    protected PlayerHealthUI healthUI;
    protected SpriteRenderer spriterenderer;

    private KeyCode keyLeft = KeyCode.LeftArrow;
    private KeyCode keyRight = KeyCode.RightArrow;
    private KeyCode keyJump = KeyCode.Space;
    private KeyCode keyAtk = KeyCode.Z;

    protected float curTime;
    protected float coolTime = 0.3f;

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

    protected virtual void OnDamaged()
    {
        if (isInvincibleTime)
            return;

        Manager.canInput = false;
        // 맞는 소리 출력

        if (health > 1)
        {
            health -= 1;
            StartCoroutine(Invincible(1.0f));
        }
        else
        {
            health -= 1;
            gameObject.SetActive(false);
            Destroy(gameObject);
            // 게임 오버 출력
        }
    }

    private IEnumerator Invincible(float invincibleTime)
    {
        isInvincibleTime = true;
        StartCoroutine(BlinkEffect(invincibleTime, spriterenderer));
        yield return new WaitForSeconds(invincibleTime);
        Manager.canInput = true;
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
