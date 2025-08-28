using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPurum : Player
{
    private EnemyBoss boss;

    void OnEnable()
    {
        moveSpeed *= 1.5f;
        UIId = 1;
        coolSkillTime = 30.0f;

        GameObject playerUI = GameObject.Find("PlayerUI");
        healthUI = playerUI.GetComponent<PlayerHealthUI>();
        healthUI.DrawUI(UIId, health);

        boss = GameObject.FindWithTag("Enemy").GetComponent<EnemyBoss>();

        bulletPrefab = Resources.Load<GameObject>("Prefab/PlayerBullet/BlueBullet");
        bulletPool = GameObject.FindWithTag("Pool");

        spriterenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();

        Manager.Instance.player = this.gameObject;

        Manager.Input.keyAction += PlayerMove;
        Manager.Input.keyAction += PlayerJump;
        Manager.Input.keyAction += PlayerAttack;
        Manager.Input.keyAction += PlayerSkill;
    }

    void OnDisable()
    {
        Manager.Instance.player = null;

        Manager.Input.keyAction -= PlayerMove;
        Manager.Input.keyAction -= PlayerJump;
        Manager.Input.keyAction -= PlayerAttack;
        Manager.Input.keyAction -= PlayerSkill;
    }

    protected override void PlayerSkill()
    {
        base.PlayerSkill();

        if (curSkillTime <= 0)
        {
            if (Input.GetKeyDown(keySkill))
            {
                StartCoroutine(StunBoss());
                curSkillTime = coolSkillTime;
            }
        }
        curSkillTime -= Time.deltaTime;
    }

    private IEnumerator StunBoss()
    {
        playerVFX.SetActive(true);
        boss.ApplyStun();
        yield return new WaitForSeconds(5f);
        playerVFX.SetActive(false);
    }
}
