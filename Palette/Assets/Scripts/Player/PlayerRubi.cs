using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRubi : Player
{
    void OnEnable()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefab/PlayerBullet/RedBullet");
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
}
