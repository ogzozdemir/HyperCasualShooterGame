using UnityEngine;

namespace Gameplay
{
    public class EnemyWeaponController : Weapon
    {
        [Space(5), Header("References"), Space(15)]
        private PlayerController player;
        private EnemyController enemyController;
    
        private void Start()
        {
            // Find the GameObject with the PlayerController component in the scene
            player = FindObjectOfType<PlayerController>();
            enemyController = transform.parent.parent.GetComponent<EnemyController>();
        }

        protected override void SpawnBullet()
        {
            if (!player.isAlive()) return;
            
            // Check if the player is alive and in range
            if (Vector3.Distance(player.transform.position, transform.position) <= enemyController.enemyRange)
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
                    Vector3 directionToPlayer = (new Vector3(
                        player.transform.position.x,
                        player.transform.position.y + .5f,
                        player.transform.position.z) - weaponBarrel.position).normalized;
                    bullet.SetVariables(directionToPlayer, bulletSpeed, bulletDamage);
            
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
    }
}