using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask isLayer;
    [SerializeField] private Transform rayStartPosition;
    public float attackPower;

    private RaycastHit2D ray;
    private Rigidbody2D bulletRigidbody;
    private float angle;
    private IEnumerator bulletDestroyCoroutine;
    private bool isInitOnce = true;

    void OnEnable()
    {
        SetBullet();

        // 쏘는 사운드 삽입
    }

    void Update()
    {
        AttackInstantiate();
    }

    private void SetBullet(float bulletHoldingTime = 2.0f)
    {
        if (bulletDestroyCoroutine != null)
            StopCoroutine(bulletDestroyCoroutine);

        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletRigidbody.gravityScale = 0.0f;
        bulletRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        bulletDestroyCoroutine = BulletDestroy(bulletHoldingTime);
        StartCoroutine(bulletDestroyCoroutine);
    }

    private IEnumerator BulletDestroy(float bulletHoldingTime = 2.0f)
    {
        isInitOnce = true;
        yield return new WaitForSeconds(bulletHoldingTime);
        angle = 0.0f;
        bulletRigidbody.velocity = Vector2.zero;
        Manager.Pool.Push(Utils.GetOrAddComponent<Poolable>(this.gameObject));
    }

    private void AttackInstantiate()
    {
        this.ray = Physics2D.Raycast(rayStartPosition.position, transform.right, rayDistance, isLayer);

        Debug.DrawRay(rayStartPosition.position, transform.right * rayDistance, Color.red);

        BulletMovement();

        ColliderCheck();
    }

    private void BulletMovement()
    {
        if (isInitOnce)
        {
            isInitOnce = false;

            if (transform.rotation.y == 0)
            {
                angle = 0;
                bulletRigidbody.velocity = new Vector2(-speed, 0);
            }
            else
            {
                angle = 180;
                bulletRigidbody.velocity = new Vector2(speed, 0);
            }
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private void ColliderCheck()
    {
        if (ray.collider != null)
        {
            if (ray.collider.tag == "Enemy")
            {
                // 여기에 피해 공식 기입
                StartCoroutine(bulletDestroyCoroutine);
            }
        }
    }
}
