using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathVFX : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }


}
