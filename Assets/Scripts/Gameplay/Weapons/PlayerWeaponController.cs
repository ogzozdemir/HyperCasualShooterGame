using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerWeaponController : Weapon
    {
        [Space(5), Header("References"), Space(15)]
        private PlayerController player;
        [HideInInspector] public List<Transform> enemies = new List<Transform>();
        [HideInInspector] public Transform nearestEnemy;

        private void Awake() => player = FindObjectOfType<PlayerController>();

        private void Start()
        {
            // Find all game objects with the "Enemy" tag
            GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyObjects)
            {
                enemies.Add(enemy.transform);
            }
        }

        protected override void SpawnBullet()
        {
            if (!player.isAlive()) return;
            
            Transform enemy = GetNearestEnemy();

            // Check if an enemy is found
            if (enemy)
            {
                if (shootDelayTimer > 0f)
                {
                    shootDelayTimer -= Time.deltaTime;
                }
                else
                {
                    // Get a bullet from object pool
                    BulletController bullet = ObjectPool.Instance.GetPooledBullet();
                    
                    // Get direction
                    Vector3 directionToNearestEnemy = (new Vector3(
                        enemy.position.x,
                        enemy.position.y + .5f,
                        enemy.position.z) - weaponBarrel.position).normalized;
                    bullet.SetVariables(directionToNearestEnemy, bulletSpeed, bulletDamage);
            
                    // Activate bullet
                    bullet.transform.position = weaponBarrel.position;
                    bullet.gameObject.SetActive(true);
                    
                    // Play audio
                    player.PlayAudio();
                    
                    // Reset shoot delay timer
                    shootDelayTimer = shootDelay;
                }
            }
            else
            {
                // Reset shoot delay timer if no enemy is found
                shootDelayTimer = 0f;
            }
        }

        private Transform GetNearestEnemy()
        {
            // Get the nearest enemy and return it
            
            nearestEnemy = null;
            float range = shootRange;

            foreach (Transform enemy in enemies)
            {
                float distanceToPlayer = Vector3.Distance(player.transform.position, enemy.transform.position);
                if (distanceToPlayer < range)
                {
                    range = distanceToPlayer;
                    nearestEnemy = enemy.transform;
                }
            }

            return nearestEnemy;
        }
    }
}

