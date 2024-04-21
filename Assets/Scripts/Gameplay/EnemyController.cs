using System;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class EnemyController : MonoBehaviour, IHumanoid
    {
        [Space(5), Header("References"), Space(15)]
        private Rigidbody rb;
        private Animator animator;
        private PlayerController player;
        
        [Space(5), Header("Enemy Variables"), Space(15)] [SerializeField]
        private float enemyHealth;
        private float EnemyHealth
        {
            get { return enemyHealth; }
            set
            {
                enemyHealth = value;

                if (enemyHealth <= 0f)
                {
                    GameObject powerUpObj = ObjectPool.Instance.GetPooledPowerUpObject();
                    powerUpObj.SetActive(true);
                    powerUpObj.transform.position = new Vector3(transform.position.x, .5f, transform.position.z);
                    
                    GameController.Instance.IncreaseScorePoints();
                    
                    player.weapon.enemies.Remove(transform);
                    gameObject.SetActive(false);
                }
            }
        }
        [SerializeField] private float enemySpeed;
        public float enemyRange;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            player = FindObjectOfType<PlayerController>();
        }

        private void OnEnable() => enemyHealth = Random.Range(100, 100 * GameController.Instance.gameLevel);
        private void Update() => MovementAnimations();
        private void FixedUpdate() => Movement();

        public void Movement()
        {
            if (!player.isAlive()) return;
            
            // Check if the distance between player and enemy is greater than enemy range and move towards the player
            if (Vector3.Distance(player.transform.position, transform.position) > enemyRange)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                rb.AddForce(direction * enemySpeed, ForceMode.Force);
            }

            Rotate();
        }

        public void Rotate()
        {
            // Rotate towards player
            Vector3 targetPosition = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
            transform.LookAt(targetPosition);
        }

        public void MovementAnimations()
        {
            // Changing the animator state parameter based on the velocity
            animator.SetBool("isRunning", !(rb.velocity.magnitude <= .1f));
        }

        public void OnTriggerEnter(Collider other)
        {
            // Check if the collided game object has a BulletController component
            if (other.gameObject.TryGetComponent<BulletController>(out BulletController bullet))
            {
                EnemyHealth -= bullet.bulletDamage;
                bullet.gameObject.SetActive(false);
            }
        }
    }
}
