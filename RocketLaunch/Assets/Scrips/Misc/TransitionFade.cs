using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TransitionFade : MonoBehaviour
{
    private const float opaqueAlphaValue = 1;
    private const float transparentAlphaValue = 0;
    public static TransitionFade Instance { get; private set; }

    [Header("Transition Fade")]
    [SerializeField,Min(0f)] private float fadeDuration = 1f;

    private Image fadeImage;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        fadeImage = GetComponent<Image>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeRoutine(transparentAlphaValue, opaqueAlphaValue));
    }

    public void FadeOut()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeRoutine(opaqueAlphaValue,transparentAlphaValue));
    }

    private IEnumerator FadeRoutine(float startingAlpha, float targetAlpha)
    {        
        float timer = 0f;
        Color imageColor = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;
            imageColor.a = Mathf.Lerp(startingAlpha, targetAlpha, progress);
            fadeImage.color = imageColor;
            yield return null;
        }
    }
}
