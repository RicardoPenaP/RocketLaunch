using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    [Header("UI Bar")]
    [SerializeField] private Image fill;

    private Material fillMaterial;

    private void Awake()
    {
        fillMaterial = fill.material;
    }

    public void UpdateFill(float currentValue, float maxValue)
    {
        fill.fillAmount = currentValue / maxValue;
    }


}
