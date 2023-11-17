using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnginePowerBar : MonoBehaviour
{
    [Header("EnginePowerBar")]
    [SerializeField] private Image fill;

    private Material fillMaterial;

    private void Awake()
    {
        fillMaterial = fill.material;
    }

    private void UpdateFill(float currentValue, float maxValue)
    {
        fill.fillAmount = currentValue / maxValue;
    }


}
