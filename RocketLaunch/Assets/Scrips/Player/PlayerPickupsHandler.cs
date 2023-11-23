using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPickupsHandler : MonoBehaviour
{
    private PlayerCollisionHandler playerCollisionHandler;
    public Action<Pickup> OnPickupPiked;

    private void Awake()
    {
        playerCollisionHandler = GetComponentInParent<PlayerCollisionHandler>();
    }

    private void Start()
    {
        if (playerCollisionHandler)
        {
            playerCollisionHandler.OnTriggerEnterWithObject += PlayerCollisionHandler_OnTriggerEnterWithObject;
        }
    }

    private void OnDestroy()
    {
        if (playerCollisionHandler)
        {
            playerCollisionHandler.OnTriggerEnterWithObject -= PlayerCollisionHandler_OnTriggerEnterWithObject;
        }
    }

    private void PlayerCollisionHandler_OnTriggerEnterWithObject(object sender, EventArgs e)
    {
        if (e is PlayerCollisionHandler.CollisionInfo<Pickup> collisionInfo)
        {
            OnPickupPiked?.Invoke(collisionInfo.collisionObject);
        }
    }
}
