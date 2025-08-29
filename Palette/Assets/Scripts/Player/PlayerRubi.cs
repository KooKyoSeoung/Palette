using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRubi : Player
{
    public static bool isAtkAdvenced = false;

    void OnEnable()
    {
        UIId = 0;
        coolSkillTime = 30.0f;
        isAtkAdvenced = false;

        GameObject playerUI = GameObject.Find("PlayerUI");
        healthUI = playerUI.GetComponent<PlayerHealthUI>();
        healthUI.DrawUI(UIId, health);

        bulletPrefab = Resources.Load<GameObject>("Prefab/PlayerBullet/RedBullet");
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
                StartCoroutine(AdvenceAtk());
                curSkillTime = coolSkillTime;
            }
        }
        curSkillTime -= Time.deltaTime;
    }

    private IEnumerator AdvenceAtk()
    {
        Manager.Sound.PlaySFX("SkillRubi");
        isAtkAdvenced = true;
        playerVFX.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        playerVFX.SetActive(false);
        isAtkAdvenced = false;
    }
}
