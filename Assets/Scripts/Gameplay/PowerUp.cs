using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class PowerUp : MonoBehaviour
    {
        public enum PowerUps
        {
            IncreaseHealth,
            IncreaseDamage,
            IncreaseWeaponSpeed
        }
        
        [Space(5), Header("Power Up"), Space(15)]
        private PowerUps powerUp;
        private float powerUpPoints;
        private TMP_Text powerUpText;

        private void Awake() => powerUpText = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();

        private void OnEnable()
        {
            // Select random power up and its stats
            System.Random random = new System.Random();
            powerUp = (PowerUps)random.Next(Enum.GetNames(typeof(PowerUps)).Length);
            
            // Set power up text
            switch (powerUp)
            {
                case PowerUps.IncreaseHealth:
                    powerUpPoints = Random.Range(15, 35);
                    powerUpText.text = "+" + powerUpPoints + "\n" + "Health";
                    break;
                
                case PowerUps.IncreaseDamage:
                    powerUpPoints = Random.Range(1, 10);
                    powerUpText.text = "+" + powerUpPoints + "\n" + "Damage";
                    break;
                
                case PowerUps.IncreaseWeaponSpeed:
                    powerUpPoints = Random.Range(.05f, .1f);
                    powerUpText.text = "+" + powerUpPoints.ToString("F2") + "\n" + "Attack Speed";
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if player is collided with power up
            if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
            {
                ObtainPowerUp(playerController);
                gameObject.SetActive(false);
            }
        }

        private void ObtainPowerUp(PlayerController player)
        {
            // Give power up features
            switch (powerUp)
            {
                case PowerUps.IncreaseHealth:
                    player.PlayerHealth += powerUpPoints;
                    break;
                
                case PowerUps.IncreaseDamage:
                    player.weapon.bulletDamage += powerUpPoints;
                    break;
                
                case PowerUps.IncreaseWeaponSpeed:
                    if (player.weapon.shootDelay > .25f)
                        player.weapon.shootDelay -= powerUpPoints;
                    else
                        player.weapon.shootDelay = .25f;
                    break;
            }
        }
    }

}
