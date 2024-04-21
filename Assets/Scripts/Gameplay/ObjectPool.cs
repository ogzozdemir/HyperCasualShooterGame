using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance { get; private set; }
    
        [Space(5), Header("References"), Space(15)]
        [SerializeField] private GameObject bulletPrefab;
        private List<BulletController> pooledBullets = new List<BulletController>();
        
        [SerializeField] private GameObject enemyPrefab;
        private List<GameObject> pooledEnemies = new List<GameObject>();
        
        [SerializeField] private GameObject powerUpPrefab;
        private List<GameObject> pooledPowerUps = new List<GameObject>();

        private void Awake() => Instance = this;

        private void Start()
        {
            // Instantiate bullets and add the BulletController component of the bullet to the pool list
            for (int i = 0; i < 25; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.SetActive(false);
            
                pooledBullets.Add(bullet.GetComponent<BulletController>());
            }
            
            // Instantiate enemies and add them to the pool list
            for (int i = 0; i < 15; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab);
                enemy.SetActive(false);
            
                pooledEnemies.Add(enemy);
            }
            
            // Instantiate power ups and add them to the pool list
            for (int i = 0; i < 15; i++)
            {
                GameObject powerUp = Instantiate(powerUpPrefab);
                powerUp.SetActive(false);
            
                pooledPowerUps.Add(powerUp);
            }
        }
    
        public BulletController GetPooledBullet()
        {
            // Iterate through the pooled bullets and return the BulletController component of the bullet
        
            for (int i = 0; i < pooledBullets.Count; i++)
            {
                if (!pooledBullets[i].gameObject.activeInHierarchy)
                    return pooledBullets[i];
            }
        
            // If no inactive bullet is found, instantiate a new one
            GameObject bullet = Instantiate(bulletPrefab);

            if (bullet.TryGetComponent<BulletController>(out BulletController bulletController))
            {
                pooledBullets.Add(bulletController);
                return bulletController;
            }

            return null;
        }
        
        public GameObject GetPooledEnemy()
        {
            // Iterate through the pooled enemies and return them
        
            for (int i = 0; i < pooledEnemies.Count; i++)
            {
                if (!pooledEnemies[i].gameObject.activeInHierarchy)
                    return pooledEnemies[i];
            }
        
            // If no inactive enemy is found, instantiate a new one
            GameObject enemy = Instantiate(bulletPrefab);
            return enemy;
        }
        
        public GameObject GetPooledPowerUpObject()
        {
            // Iterate through the pooled power up objects and return them
        
            for (int i = 0; i < pooledPowerUps.Count; i++)
            {
                if (!pooledPowerUps[i].gameObject.activeInHierarchy)
                    return pooledPowerUps[i];
            }
        
            // If no inactive power up is found, instantiate a new one
            GameObject powerUp = Instantiate(powerUpPrefab);
            return powerUp;
        }
    }
}

