using System;
using Gameplay;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [Space(5), Header("References"), Space(15)]
        [SerializeField] private GameObject gameScreen;
        [SerializeField] private TMP_Text scorePoints;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text levelText;
        
        [SerializeField] private GameObject endScreen;
        [SerializeField] private TMP_Text endStatsText;
        
        private void Start()
        {
            // Activate the game screen
            gameScreen.SetActive(true);
            endScreen.SetActive(false);
            
            // Subscribe to GameEnded event
            PlayerController.GameEnded += OnGameEnded;
        }

        public void UpdateScorePoints(int points) => scorePoints.text = points.ToString();
        public void UpdateHealth(float health) => healthText.text = "HEALTH: " + "<b>" + Math.Round(health) + "</b>";
        public void IncreaseLevel(int level) => levelText.text = "LEVEL: " + "<b>" + level + "</b>";

        private void OnGameEnded()
        {
            // Activate the end screen
            gameScreen.SetActive(false);
            endScreen.SetActive(true);
        }
        
        public void DrawEndGameStats(int gameLevel, int score)
        {
            // Draw end game stats
            endStatsText.text =
                "YOUR LEVEL WAS " + "<b>" + gameLevel + "</b>"
                + "\n" +
                "AND YOUR SCORE WAS " + "<b>" + score + "</b>"
                ;
        }
        
        // Unsubscribe to GameEnded event
        private void OnDestroy() =>  PlayerController.GameEnded -= OnGameEnded;
    }
}
