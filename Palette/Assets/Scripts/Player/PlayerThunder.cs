using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThunder : Player
{
    private EnemyBoss boss;

    void OnEnable()
    {
        UIId = 2;
        coolSkillTime = 15.0f;

        GameObject playerUI = GameObject.Find("PlayerUI");
        healthUI = playerUI.GetComponent<PlayerHealthUI>();
        healthUI.DrawUI(UIId, health);

        boss = GameObject.FindWithTag("Enemy").GetComponent<EnemyBoss>();

        bulletPrefab = Resources.Load<GameObject>("Prefab/PlayerBullet/YellowBullet");
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

    void Update()
    {
        CheatHeal();
    }

    protected override void PlayerSkill()
    {
        base.PlayerSkill();

        if (curSkillTime <= 0)
        {
            if (Input.GetKeyDown(keySkill))
            {
                StartCoroutine(ShowWeakPoint());
                curSkillTime = coolSkillTime;
            }
        }
        curSkillTime -= Time.deltaTime;
    }

    private IEnumerator ShowWeakPoint()
    {
        Manager.Sound.PlaySFX("SkillThunder");
        playerVFX.SetActive(true);
        boss.ShowWeakPoint();
        yield return new WaitForSeconds(7.0f);
        playerVFX.SetActive(false);
    }
}
