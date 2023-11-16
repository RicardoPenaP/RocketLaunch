using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCollisionHandler : MonoBehaviour
{
    public class CollisionInfo<T> : EventArgs where T : MonoBehaviour
    {
        public T collisionObject;

        public CollisionInfo( T collisionObject)
        {
            this.collisionObject = collisionObject;
        }
    }

    public event EventHandler OnCollisionEnterWithObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<LevelPlatform>(out LevelPlatform levelPlatform))
        {
            OnCollisionEnterWithObject?.Invoke(this, new CollisionInfo<LevelPlatform>(levelPlatform));
        }

        if (collision.transform.TryGetComponent<ObstacleController>(out ObstacleController obstacleController))
        {
            OnCollisionEnterWithObject?.Invoke(this, new CollisionInfo<ObstacleController>(obstacleController));
        }
        
    }



}
