using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class EnemySpawner : MonoBehaviour
    {
        [Space(5), Header("References"), Space(15)]
        private PlayerController player;
        private PlayerWeaponController playerWeapon;

        [Space(5), Header("Variables"), Space(15)]
        private float spawnTimer = 1f;
        private float timer = 0f;

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>();
            playerWeapon = FindObjectOfType<PlayerWeaponController>();
        }

        private void Update()
        {
            if (!player.isAlive()) return;
            
            // Instantiate an enemy from the object pool at a random position within the spawner's local scale
            timer += Time.deltaTime;
        
            if (timer >= spawnTimer)
            {
                Vector3 randomPos = new Vector3(
                    Random.Range(-transform.localScale.x * 5f, transform.localScale.x * 5f), 
                    0f, 
                    Random.Range(-transform.localScale.z * 5f, transform.localScale.z * 5f));

                GameObject enemy = ObjectPool.Instance.GetPooledEnemy();
                enemy.transform.position = randomPos;
                enemy.SetActive(true);
                
                // Add the enemy's transform to the player weapon's list of enemies
                playerWeapon.enemies.Add(enemy.transform);

                timer = 0f;
                spawnTimer = Random.Range(3f, 6f);
            }
        }
    }
}