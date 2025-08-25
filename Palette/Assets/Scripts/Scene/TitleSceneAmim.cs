using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleSceneAmim : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private RectTransform backgroundImage;
    [SerializeField] private GameObject[] buttons = new GameObject[3];
    private Image[] buttonImgs = new Image[3];

    private Color originColor = new Color(1, 1, 1, 1);
    private Color blackColor = new Color(0, 0, 0, 1);
    private Color transColor = new Color(0, 0, 0, 0);

    void Start()
    {
        fadeImage.color = blackColor;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
            buttonImgs[i] = buttons[i].GetComponent<Image>();
            buttonImgs[i].color = transColor;
        }

        Manager.Sound.PlayBGM("Title");

        StartCoroutine(StartScreenAnim());
    }

    private IEnumerator StartScreenAnim()
    {
        fadeImage.DOColor(transColor, 1.0f);
        yield return new WaitForSeconds(1.5f);

        backgroundImage.DOMoveY(190, 1.5f);
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
            buttonImgs[i].DOColor(originColor, 1.0f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
