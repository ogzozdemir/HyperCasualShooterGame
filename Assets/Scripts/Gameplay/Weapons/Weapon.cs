using UnityEngine;

namespace Gameplay
{
    public abstract class Weapon : MonoBehaviour
    {
        [Space(5), Header("References"), Space(15)]
        [SerializeField] protected Transform weaponBarrel;
    
        [Space(5), Header("Weapon Variables"), Space(15)]
        [SerializeField] protected float shootRange;
        public float shootDelay;
        
        protected float shootDelayTimer = 0f;
        
        public float bulletDamage;
        [SerializeField] protected float bulletSpeed;
        
        protected void Update() => SpawnBullet();
        protected virtual void SpawnBullet() { }
    }
}
