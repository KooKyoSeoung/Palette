using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeInSequence : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private float delayBetween = 0.5f;
    private CanvasGroup[] elements;

    void Awake()
    {
        elements = GetComponentsInChildren<CanvasGroup>(true);
    }

    void OnEnable()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].alpha = 0;
        }

        StartCoroutine(FadeInSequence());
    }

    private IEnumerator FadeInSequence()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            yield return StartCoroutine(FadeIn(elements[i]));
            yield return new WaitForSeconds(delayBetween);
        }
    }

    private IEnumerator FadeIn(CanvasGroup cg)
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }
        cg.alpha = 1;
    }
}
