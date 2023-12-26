using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCollisionHandler : MonoBehaviour
{
    private const float COLLISION_REST_TIME = 0.1f;

    private bool canColide = true;

    public class CollisionInfo<T> : EventArgs where T : MonoBehaviour
    {
        public T collisionObject;

        public CollisionInfo( T collisionObject)
        {
            this.collisionObject = collisionObject;
        }
    }

    public event EventHandler OnCollisionEnterWithObject;
    public event EventHandler OnTriggerEnterWithObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (!canColide)
        {
            return;
        }

        if (collision.transform.TryGetComponent<LevelPlatform>(out LevelPlatform levelPlatform))
        {
            OnCollisionEnterWithObject?.Invoke(this, new CollisionInfo<LevelPlatform>(levelPlatform));
        }

        if (collision.transform.TryGetComponent<ObstacleController>(out ObstacleController obstacleController))
        {
            OnCollisionEnterWithObject?.Invoke(this, new CollisionInfo<ObstacleController>(obstacleController));
        }

        StartCoroutine(CollisionRestRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Pickup>(out Pickup pickup))
        {
            OnTriggerEnterWithObject?.Invoke(this, new CollisionInfo<Pickup>(pickup));
        }
    }

    private IEnumerator CollisionRestRoutine()
    {
        canColide = false;
        yield return new WaitForSeconds(COLLISION_REST_TIME);
        canColide = true;
    }



}
