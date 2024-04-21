using System;
using UI;
using UnityEngine;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour, IHumanoid
    {
        [Space(5), Header("References"), Space(15)]
        private Rigidbody rb;
        private Animator animator;
        private UIController uiController;
        private AudioSource audioSource;
        public PlayerWeaponController weapon;
        [SerializeField] private Joystick joystick;
        [SerializeField] private AudioClip audioClip;
        public static event Action GameEnded;

        [Space(5), Header("Player Variables"), Space(15)]
        private float playerHealth = 100f;
        public float PlayerHealth
        {
            get { return playerHealth; }
            set {
                playerHealth = value;

                uiController.UpdateHealth(playerHealth);
                
                if (playerHealth <= 0f)
                    GameEnded?.Invoke();
                else if (playerHealth >= 100)
                    playerHealth = 100;
            }
        }
        [SerializeField] private float playerSpeed;
        [SerializeField] private float rotationSpeed;
        [HideInInspector] public Vector3 direction;

        public bool isAlive()
        {
            // Return if player is alive or not
            return PlayerHealth > 0;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            uiController = FindObjectOfType<UIController>();
            audioSource = GetComponent<AudioSource>();
        }
        
        private void Update() => MovementAnimations();
        private void FixedUpdate() => Movement();

        public void Movement()
        {
            if (!isAlive()) return;
            
            // Moving the player based on the joystick input
            direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical).normalized;
            rb.AddForce(direction * (playerSpeed * Time.fixedDeltaTime), ForceMode.VelocityChange);
            
            // Limiting the speed
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, playerSpeed);
            
            Rotate();
        }

        public void MovementAnimations()
        {
            // Changing the animator state parameter based on the velocity
            animator.SetBool("isRunning", !(rb.velocity.magnitude <= .1f));
        }

        public void Rotate()
        {
            // Rotating the character based on direction and nearest enemy
            if (!weapon.nearestEnemy)
            {
                if (direction != Vector3.zero) {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * rotationSpeed);
                }
            }
            else
            {
                Vector3 targetPosition = new Vector3(weapon.nearestEnemy.position.x, 0f, weapon.nearestEnemy.position.z);
                transform.LookAt(targetPosition);
            }
        }
        
        public void OnTriggerEnter(Collider other)
        {
            // Check if the collided game object has a BulletController component
            if (other.gameObject.TryGetComponent<BulletController>(out BulletController bullet))
            {
                PlayerHealth -= bullet.bulletDamage;
                bullet.gameObject.SetActive(false);
            }
        }
        
        public void PlayAudio() => audioSource.PlayOneShot(audioClip);
    }
}