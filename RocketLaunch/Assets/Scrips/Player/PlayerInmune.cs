using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInmune : MonoBehaviour
{
    [Header("Player Inmune")]
    [Header("Rocket Body References")]
    [SerializeField] private MeshRenderer bodyMeshRenderer;
    [SerializeField] private MeshRenderer glassMeshRenderer;

    [Header("Inmune Settings")]
    [SerializeField] private float inmuneTime = 1f;
    [SerializeField] private float blinkSpeed = 10f;    
    [SerializeField] private int amountOfBlinks = 8;
    

    private PlayerController playerController;
    private Material bodyMaterial;
    private Material glassMaterial;
   
    public bool IsInmune { get; private set; }

    private void Awake()
    {
        IsInmune = false;
        playerController = GetComponent<PlayerController>();
        bodyMaterial = bodyMeshRenderer.sharedMaterial;
        glassMaterial = glassMeshRenderer.sharedMaterial;
    }

    private void Start()
    {
        playerController.OnPlayerReset += PlayerController_OnPlayerReset;
    }

    private void OnDestroy()
    {
        playerController.OnPlayerReset -= PlayerController_OnPlayerReset;
    }

    private void PlayerController_OnPlayerReset(object sender, EventArgs e)
    {
        StartCoroutine(InmuneRoutine());
    }

    private IEnumerator InmuneRoutine()
    {
        IsInmune = true;
        float timer = 0f;
        float blinkDirectionTimer = 0f;
        float blinkDirection = -1f;
        float blinkDirectionChangeTime = inmuneTime / amountOfBlinks;

        Color bodyMaterialColor = bodyMaterial.color;
        Color glassMaterialColor = glassMaterial.color;

        while (timer < inmuneTime)
        {
            timer += Time.deltaTime;
            blinkDirectionTimer += Time.deltaTime;
            bodyMaterialColor = bodyMaterial.color;
            glassMaterialColor = glassMaterial.color;

            bodyMaterialColor.a = Mathf.Clamp(bodyMaterialColor.a + (blinkDirection * blinkSpeed * Time.deltaTime), 0, 1);
            glassMaterialColor.a = Mathf.Clamp(glassMaterialColor.a + (blinkDirection * blinkSpeed * Time.deltaTime), 0, 1);

            bodyMaterial.color = bodyMaterialColor;
            glassMaterial.color = glassMaterialColor;

            if (blinkDirectionTimer >= blinkDirectionChangeTime)
            {
                blinkDirectionTimer = 0f;
                blinkDirection *= -1;
            }
            yield return null;
        }

        bodyMaterialColor.a = 1f;
        glassMaterialColor.a = 1f;

        bodyMaterial.color = bodyMaterialColor;
        glassMaterial.color = glassMaterialColor;

        IsInmune = false;
    }
}
