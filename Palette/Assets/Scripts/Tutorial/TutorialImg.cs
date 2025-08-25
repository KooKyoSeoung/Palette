using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialImg : MonoBehaviour
{
    const float MIN_DIST = 6.0f;
    const float REF_DIST = 15.0f;

    private SpriteRenderer sprite;

    private Transform playerTransform;
    private float playerDistance;

    private float spriteAlpha;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

        sprite = gameObject.GetComponent<SpriteRenderer>();

        sprite.color = new Color(1, 1, 1, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDistance = Vector2.Distance(gameObject.transform.position, playerTransform.position);

            if (playerDistance < MIN_DIST)
                sprite.color = new Color(1, 1, 1, 1);
            else
            {
                spriteAlpha = (REF_DIST - playerDistance) / 10.0f;
                sprite.color = new Color(1, 1, 1, spriteAlpha);
            }
        }
    }
}
