using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    private TextMeshPro text;

    private Transform playerTransform;
    private float playerDistance;

    private float textAlpha;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

        text = gameObject.GetComponent<TextMeshPro>();

        text.color = new Color(0, 0, 0, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDistance = Vector2.Distance(gameObject.transform.position, playerTransform.position);

            if (playerDistance < 10.0f)
                text.color = new Color(0, 0, 0, 1);
            else
            {
                textAlpha = (20.0f - playerDistance) / 10.0f;
                text.color = new Color(0, 0, 0, textAlpha);
            }
        }
    }
}
