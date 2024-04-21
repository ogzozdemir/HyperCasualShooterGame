using UnityEngine;

namespace Gameplay
{
    public interface IHumanoid
    {
        public void Movement();
        public void MovementAnimations();
        public void Rotate();
        public void OnTriggerEnter(Collider other);
    }
}