using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected GameObject bulletPrefab;
    protected GameObject bulletPool;

    protected Rigidbody2D rigidbody;

    private KeyCode keyLeft = KeyCode.LeftArrow;
    private KeyCode keyRight = KeyCode.RightArrow;
    private KeyCode keyJump = KeyCode.Space;
    private KeyCode keyAtk = KeyCode.Z;

    protected float curTime;
    protected float coolTime = 0.3f;

    protected float moveSpeed = 6.0f;
    protected float jumpPower = 15.0f;
    protected float horizontalMovement = 0.0f;

    protected bool isGrounded = false;

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
}
