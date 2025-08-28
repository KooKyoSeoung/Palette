using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClover : Player
{
    void OnEnable()
    {
        maxHealth = 5;
        health = 5;
        UIId = 3;
        coolSkillTime = 30.0f;

        GameObject playerUI = GameObject.Find("PlayerUI");
        healthUI = playerUI.GetComponent<PlayerHealthUI>();
        healthUI.DrawUI(UIId, health);

        bulletPrefab = Resources.Load<GameObject>("Prefab/PlayerBullet/GreenBullet");
        bulletPool = GameObject.FindWithTag("Pool");

        spriterenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();

        Manager.Instance.player = this.gameObject;

        Manager.Input.keyAction += PlayerMove;
        Manager.Input.keyAction += PlayerJump;
        Manager.Input.keyAction += PlayerAttack;
    }

    void OnDisable()
    {
        Manager.Instance.player = null;

        Manager.Input.keyAction -= PlayerMove;
        Manager.Input.keyAction -= PlayerJump;
        Manager.Input.keyAction -= PlayerAttack;
    }
}
