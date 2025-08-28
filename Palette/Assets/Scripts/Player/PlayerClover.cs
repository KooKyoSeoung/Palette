using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClover : Player
{
    private const int UI_ID = 3;

    void OnEnable()
    {
        health = 5;

        GameObject playerUI = GameObject.Find("PlayerUI");
        healthUI = playerUI.GetComponent<PlayerHealthUI>();
        healthUI.DrawUI(UI_ID, health);

        bulletPrefab = Resources.Load<GameObject>("Prefab/PlayerBullet/GreenBullet");
        bulletPool = GameObject.FindWithTag("Pool");

        rigidbody = GetComponent<Rigidbody2D>();

        Manager.Input.keyAction += PlayerMove;
        Manager.Input.keyAction += PlayerJump;
        Manager.Input.keyAction += PlayerAttack;
    }

    void OnDisable()
    {
        Manager.Input.keyAction -= PlayerMove;
        Manager.Input.keyAction -= PlayerJump;
        Manager.Input.keyAction -= PlayerAttack;
    }

    protected override void OnDamaged()
    {
        base.OnDamaged();

        healthUI.DrawUI(UI_ID, health);
    }
}
