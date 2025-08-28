using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    private KeyCode moveLeftKey = KeyCode.LeftArrow;
    private KeyCode moveRightKey = KeyCode.RightArrow;
    private KeyCode jumpKey = KeyCode.Space;

    private float moveSpeed = 6.0f;
    private float jumpPower = 20.0f;
    private float horizontalMovement = 0.0f;

    private bool isGrounded = true;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        Manager.Input.keyAction += PlayerMove;
        Manager.Input.keyAction += PlayerJump;
    }

    void OnDisable()
    {
        Manager.Input.keyAction -= PlayerMove;
        Manager.Input.keyAction -= PlayerJump;
    }

    private void PlayerMove()
    {
        horizontalMovement = 0.0f;

        if (Input.GetKey(moveLeftKey))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

            horizontalMovement = -1.0f;
        }
        if (Input.GetKey(moveRightKey))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

            horizontalMovement = 1.0f;
        }

        rigidbody.velocity = new Vector3(horizontalMovement * moveSpeed, rigidbody.velocity.y, 0.0f);
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            isGrounded = false;

            rigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
