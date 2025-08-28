using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletHoldingTime = 2.0f;

    void Start()
    {
        Destroy(gameObject, bulletHoldingTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().OnDamaged();

            Destroy(gameObject);
        }
    }
}
