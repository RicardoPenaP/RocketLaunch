using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExperienceBar : MonoBehaviour
{
    [Header("Player Experience Bar")]
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private float totalAnimationDurationTime = 1f;

    private Queue<PlayerExperienceData> updateVisualQueue;

    private bool isUpdating = false;

    private void Awake()
    {
        updateVisualQueue = new Queue<PlayerExperienceData>();
    }

    private void OnEnable()
    {
        if (updateVisualQueue.Count > 0 )
        {
            isUpdating = true;            
            StartCoroutine(UpdateVisualsRoutine());
        }
        else
        {
            if (RocketLevelMananger.Instance)
            {
                PlayerExperienceData playerExperienceData = RocketLevelMananger.Instance.GetActualPlayerExperienceData();
                UpdateText(playerExperienceData.currentExperience, playerExperienceData.maxExperience);
                UpdateFillBar(playerExperienceData.normalizedCurrentExperience);
            }           
        }
    }

    private void Start()
    {
        if (RocketLevelMananger.Instance)
        {
            RocketLevelMananger.Instance.OnUpdateVisuals += RocketLevelMananger_OnUpdateVisuals;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        if (RocketLevelMananger.Instance)
        {
            RocketLevelMananger.Instance.OnUpdateVisuals -= RocketLevelMananger_OnUpdateVisuals;
        }
    }

    private void RocketLevelMananger_OnUpdateVisuals(PlayerExperienceData playerExperienceData)
    {
        updateVisualQueue.Enqueue(playerExperienceData);
        if (!isUpdating && gameObject.activeInHierarchy)
        {
            isUpdating = true;
            StartCoroutine(UpdateVisualsRoutine());
        }
    }

    private void UpdateText(float currentValue, float maxValue)
    {
        experienceText.text = $"{currentValue.ToString("0")}/{maxValue.ToString("0")}";
    }

    private void UpdateFillBar(float normalizedCurrentValue)
    {
        fillImage.fillAmount = normalizedCurrentValue;
    }

    private IEnumerator UpdateVisualsRoutine()
    {
        while (updateVisualQueue.Count > 0)
        { 
            PlayerExperienceData currentPlayerExperienceData = updateVisualQueue.Dequeue();

            float timer = totalAnimationDurationTime * currentPlayerExperienceData.normalizedCurrentExperience;
            float targetTime = totalAnimationDurationTime * currentPlayerExperienceData.normalizedTargetExperience;

            UpdateFillBar(currentPlayerExperienceData.normalizedCurrentExperience);            
            UpdateText(currentPlayerExperienceData.currentExperience, currentPlayerExperienceData.maxExperience);

            while (timer < targetTime)
            {
                timer += Time.deltaTime;
                float progress = timer / totalAnimationDurationTime;                
                UpdateFillBar(progress);
                float currentValue = Mathf.Lerp(0, currentPlayerExperienceData.maxExperience, progress);
                UpdateText(currentValue, currentPlayerExperienceData.maxExperience);
                yield return null;
            }

            UpdateText(currentPlayerExperienceData.targetExperience, currentPlayerExperienceData.maxExperience);
            fillImage.fillAmount = currentPlayerExperienceData.normalizedTargetExperience;
        }

        isUpdating = false;
    }

}
