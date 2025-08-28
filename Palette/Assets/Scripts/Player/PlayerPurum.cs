using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPurum : Player
{
    private const int UI_ID = 1;

    void OnEnable()
    {
        moveSpeed *= 1.5f;

        GameObject playerUI = GameObject.Find("PlayerUI");
        healthUI = playerUI.GetComponent<PlayerHealthUI>();
        healthUI.DrawUI(UI_ID, health);

        bulletPrefab = Resources.Load<GameObject>("Prefab/PlayerBullet/BlueBullet");
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
