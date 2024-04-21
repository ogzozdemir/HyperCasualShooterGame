using UnityEngine;

namespace Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [Space(5), Header("References"), Space(15)]
        [SerializeField] private Transform playerTransform;
    
        [Space(5), Header("Camera Variables"), Space(15)]
        [SerializeField] private float yOffset;

        private void LateUpdate()
        {
            Vector3 newPosition = playerTransform.position;
            newPosition.y = playerTransform.position.y + yOffset;
            transform.position = newPosition;
        }
    }
}