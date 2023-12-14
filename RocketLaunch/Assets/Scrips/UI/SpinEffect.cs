using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinEffect : MonoBehaviour
{
    [Header("Spin Effect")]
    [SerializeField] private float spinSpeed = 10f;

    private int spinDirection = 1;

    private void Start()
    {
        spinDirection = Random.Range(0, 2) < 1 ? -1 : 1;
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * spinSpeed * spinDirection * Time.deltaTime);
    }
}
