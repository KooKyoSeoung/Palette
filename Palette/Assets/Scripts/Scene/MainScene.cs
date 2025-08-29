using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainScene : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject[] playerPrefabs = new GameObject[4];
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject[] backgrounds = new GameObject[5];
    [SerializeField] private TutorialCamera camera;
    private int playerIndex;

    [Header("UI")]
    [SerializeField] private GameObject firstInfoUI;
    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private GameObject gameclearUI;
    [SerializeField] private SpriteRenderer blackScreen;
    private bool isAppearOnce = true;
    private Color transColor = new Color(0, 0, 0, 0);
    private Color nonTransColor = new Color(0, 0, 0, 1);

    [Header("Boss")]
    [SerializeField] private EnemyBoss boss;
    
    void Start()
    {
        SetPlayer();
        gameoverUI.SetActive(false);
        gameclearUI.SetActive(false);
        blackScreen.color = transColor;

        camera.RightEndSize = 11;

        Manager.canInput = false;
        firstInfoUI.SetActive(true);
        boss.ChangeToStoped();

        Manager.Sound.PlayBGM("Boss");
    }

    void Update()
    {
        if (isAppearOnce && Manager.Instance.player == null)
        {
            isAppearOnce = false;
            Manager.Sound.PlaySFX("GameOver");
            gameoverUI.SetActive(true);
        }
    }

    private void SetPlayer()
    {
        if (PlayerSelectData.NAME == PlayerName.Hati) 
            PlayerSelectData.SetPlayerName();

        playerIndex = (int)PlayerSelectData.NAME - 1;

        if (playerIndex >= 0 && playerIndex < playerPrefabs.Length)
        {
            GameObject player = Instantiate(playerPrefabs[playerIndex], spawnPosition.position, Quaternion.identity);

            for (int i = 0; i < backgrounds.Length; i++)
            {
                if (i == playerIndex)
                    backgrounds[i].SetActive(true);
                else
                    backgrounds[i].SetActive(false);
            }
        }
        else
        {
            Debug.LogError("Player Undefined!!!");
            PlayerSelectData.NAME = PlayerName.Rubi;
            SetPlayer();
        }
    }

    public void BossDeadCall()
    {
        StartCoroutine(GameClearCoroutine());
    }

    private IEnumerator GameClearCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        Manager.Sound.PlayBGM("Title");
        if (playerIndex >= 0 && playerIndex < playerPrefabs.Length)
        {
            blackScreen.DOColor(nonTransColor, 0.5f);
            yield return new WaitForSeconds(0.5f);
            backgrounds[playerIndex].SetActive(false);
            yield return new WaitForSeconds(1.0f);
            backgrounds[backgrounds.Length - 1].SetActive(true);
            blackScreen.DOColor(transColor, 0.5f);
        }
        yield return new WaitForSeconds(5.0f);
        gameclearUI.SetActive(true);
    }

    public void FirstUIClicked()
    {
        Manager.Sound.PlaySFX("UIClick");

        firstInfoUI.SetActive(false);

        Manager.canInput = true;

        boss.ChangeToIdle();
    }

    public void GameoverContinueClicked()
    {
        Manager.Sound.PlaySFX("UIClick");

        StopAllCoroutines();
        LoadingScene.LoadScene("MainScene");
    }

    public void GameoverQuitClicked()
    {
        Manager.Sound.PlaySFX("UIClick");

        StopAllCoroutines();
        LoadingScene.LoadScene("TitleScene");
    }

    public void GameclearContinueClicked()
    {
        Manager.Sound.PlaySFX("UIClick");

        SceneManager.LoadScene("EndingScene");
    }
}
