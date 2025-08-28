using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThunder : Player
{
    void OnEnable()
    {
        UIId = 2;
        coolSkillTime = 15.0f;

        GameObject playerUI = GameObject.Find("PlayerUI");
        healthUI = playerUI.GetComponent<PlayerHealthUI>();
        healthUI.DrawUI(UIId, health);

        bulletPrefab = Resources.Load<GameObject>("Prefab/PlayerBullet/YellowBullet");
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
