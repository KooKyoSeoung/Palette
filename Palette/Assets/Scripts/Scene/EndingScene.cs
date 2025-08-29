using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{
    [SerializeField] private GameObject nextStageBtn;
    [SerializeField] private RectTransform creditList;
    [SerializeField] private float creditStartY;
    [SerializeField] private float creditEndY;
    [SerializeField] private float creditSpeed;
    [SerializeField] private float delayTime;

    private Image nextStageBtnImg;
    private Vector3 normalSpeed;
    private Color transColor = new Color(1, 1, 1, 0);
    private Color originColor = new Color(1, 1, 1, 1);

    void Start()
    {
        nextStageBtn.SetActive(false);
        nextStageBtnImg = nextStageBtn.GetComponent<Image>();
        nextStageBtnImg.color = transColor;

        var normalY = creditSpeed * Time.unscaledDeltaTime;
        normalSpeed = new Vector3(0f, normalY);

        creditList.anchoredPosition = new Vector2(0, creditStartY);

        StartCoroutine(CreditAnim());
    }

    private IEnumerator CreditAnim()
    {
        yield return new WaitForSeconds(delayTime);

        Vector3 curPos = creditList.anchoredPosition;

        while (curPos.y < creditEndY)
        {
            curPos += normalSpeed;

            creditList.anchoredPosition = curPos;

            yield return null;
        }

        yield return new WaitForSeconds(delayTime);

        nextStageBtn.SetActive(true);
        nextStageBtnImg.DOColor(originColor, 1.0f);
    }

    public void OnClickContinueBtn()
    {
        Manager.Sound.PlaySFX("UIClick");

        SceneManager.LoadScene("TitleScene");
    }
}
