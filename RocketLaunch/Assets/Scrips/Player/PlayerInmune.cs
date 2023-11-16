using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInmune : MonoBehaviour
{
    [Header("Player Inmune")]
    [Header("Inmune Settings")]
    [SerializeField] private float inmuneTime = 1f;
    [SerializeField] private float blinkSpeed = 10f;
    [SerializeField] private int amountOfBlinks = 8;

    private MeshRenderer[] meshRenderers;
    private List<Material> materials;

    private PlayerController playerController;
    private Material bodyMaterial;
    private Material glassMaterial;
   
    public bool IsInmune { get; private set; }

    private void Awake()
    {
        IsInmune = false;
        playerController = GetComponent<PlayerController>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        materials = new List<Material>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            materials.Add(meshRenderer.material);
        }
        
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

        while (timer < inmuneTime)
        {
            timer += Time.deltaTime;
            blinkDirectionTimer += Time.deltaTime;

            foreach (Material material in materials)
            {
                Color color = material.color;
                color.a = Mathf.Clamp(color.a + (blinkDirection * blinkSpeed * Time.deltaTime), 0, 1);
                material.color = color;
            }

            if (blinkDirectionTimer >= blinkDirectionChangeTime)
            {
                blinkDirectionTimer = 0f;
                blinkDirection *= -1;
            }
            yield return null;
        }

        foreach (Material material in materials)
        {
            Color color = material.color;
            color.a = 1;
            material.color = color;
        }

        IsInmune = false;
    }
}
