using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClover : Player
{
    void OnEnable()
    {
        maxHealth = 5;
        health = maxHealth;
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
        Manager.Input.keyAction += PlayerSkill;
    }

    void OnDisable()
    {
        Manager.Instance.player = null;

        Manager.Input.keyAction -= PlayerMove;
        Manager.Input.keyAction -= PlayerJump;
        Manager.Input.keyAction -= PlayerAttack;
        Manager.Input.keyAction += PlayerSkill;
    }

    protected override void PlayerSkill()
    {
        base.PlayerSkill();

        if (curSkillTime <= 0)
        {
            if (Input.GetKeyDown(keySkill))
            {
                StartCoroutine(HealPlayer());
                curSkillTime = coolSkillTime;
            }
        }
        curSkillTime -= Time.deltaTime;
    }

    private IEnumerator HealPlayer()
    {
        playerVFX.SetActive(true);
        PlayerHeal(1);
        yield return new WaitForSeconds(2f);
        playerVFX.SetActive(false);
    }
}
