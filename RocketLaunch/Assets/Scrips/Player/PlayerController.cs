using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Player Controller")]
    [SerializeField] private int maxLifesAmount = 3;    

    public event EventHandler OnLifeRemove;
    public event EventHandler OnDie;

    //For testing
    [SerializeField] private int currentLifesAmount;


    private void Awake()
    {
        currentLifesAmount = maxLifesAmount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<ObstacleController>(out ObstacleController obstacle))
        {
            RemoveOneLife();
        }
    }

    private void RemoveOneLife()
    {
        currentLifesAmount--;        
        OnLifeRemove?.Invoke(this, EventArgs.Empty);

        if (currentLifesAmount <= 0)
        {
            currentLifesAmount = 0;
            Die();
        }
    }

    private void Die()
    {
        OnDie?.Invoke(this, EventArgs.Empty);
    }

}
