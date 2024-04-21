using UnityEngine;

namespace Gameplay
{
    public class BulletController : MonoBehaviour
    {
        [Space(5), Header("References"), Space(15)]
        private Rigidbody rb;

        [Space(5), Header("Bullet Variables"), Space(15)]
        [HideInInspector] public float bulletDamage;
        private float bulletSpeed;
        private Vector3 bulletDirection;
        private float destroyTimer;

        private void Awake() => rb = GetComponent<Rigidbody>();

        public void SetVariables(Vector3 direction, float speed, float damage)
        {
            // Set bullet variables
            bulletDirection = direction;
            bulletSpeed = speed;
            bulletDamage = damage;
        
            rb.velocity = bulletDirection * bulletSpeed;
        }

        private void Update()
        {
            // Check if destroyTimer is greater than or equal to 5 seconds and deactivate the object
            destroyTimer += Time.deltaTime;

            if (destroyTimer >= 5f)
            {
                destroyTimer = 0f;
                gameObject.SetActive(false);
            }
        }
    }
}