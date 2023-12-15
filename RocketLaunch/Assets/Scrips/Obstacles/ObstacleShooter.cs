using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleShooter : MonoBehaviour
{
    [Header("Obstacle Shooter")]
    [SerializeField] private ObstacleAmmo[] obstacleAmmoPrefabs;
    [SerializeField] private Transform[] shootPositions;
    [Tooltip("The amount of shoots per second")]
    [SerializeField] private float shootRate = 1f;
    [SerializeField] private bool shootFromAllShootPositionsAtTheSameTime = false;

    private bool canShoot = false;
    private float shootRestTime;
    private int shootPositionIndex = 0;

    private void Awake()
    {
        shootRestTime = 1 / shootRate;
    }

    private void Start()
    {
        StartCoroutine(ShootRestRoutine());
    }

    private void Update()
    {
        if (canShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        canShoot = false;
        //Shoot behaviour
        if (shootFromAllShootPositionsAtTheSameTime)
        {
            foreach (Transform shootPosTranform in shootPositions)
            {
                int ammoIndex = Random.Range(0, obstacleAmmoPrefabs.Length);
                Instantiate(obstacleAmmoPrefabs[ammoIndex], shootPosTranform.position, shootPosTranform.rotation, transform.parent);
            }
        }
        else
        {
            int ammoIndex = Random.Range(0, obstacleAmmoPrefabs.Length);
            Instantiate(obstacleAmmoPrefabs[ammoIndex], shootPositions[shootPositionIndex].position, shootPositions[shootPositionIndex].rotation,transform.parent);
            shootPositionIndex = shootPositionIndex < shootPositions.Length - 1 ? shootPositionIndex + 1 : 0;
        }

        StartCoroutine(ShootRestRoutine());
    }

    private IEnumerator ShootRestRoutine()
    {
        float timer = 0;

        while (timer < shootRestTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        canShoot = true;
    }
}
