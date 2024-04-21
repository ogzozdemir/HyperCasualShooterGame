using Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        
        [Space(5), Header("References"), Space(15)]
        private UIController uiController;
        private int scorePoints;

        [Space(5), Header("Game Variables"), Space(15)]
        public int gameLevel = 1;
        [SerializeField] private int killedEnemy;
        private bool gameEnded;

        private void Awake()
        {
            // Set the frame rate to infinite
            Application.targetFrameRate = 0;
            
            Instance = this;
            uiController = FindObjectOfType<UIController>();
            
            // Subscribe to GameEnded event
            PlayerController.GameEnded += OnGameEnded;
        }

        private void Update()
        {
            // If game is ended and pressed any key, reload the game
            if (gameEnded && Input.anyKeyDown) SceneManager.LoadScene(0);
        }

        public void IncreaseScorePoints()
        {
            // Increase the score points by the given points and update the score points in the UI
            scorePoints++;
            uiController.UpdateScorePoints(scorePoints);
            
            IncreaseKilledEnemy();
        }

        public void IncreaseKilledEnemy()
        {
            // Increase the killed enemy count, check if the number of killed enemies is greater than 5 and increase the level
            killedEnemy++;

            if (killedEnemy == 5)
            {
                gameLevel++;
                uiController.IncreaseLevel(gameLevel);
                killedEnemy = 0;
            }
        }
        
        private void OnGameEnded() 
        {
            // Pass end game stats to UI Controller
            uiController.DrawEndGameStats(gameLevel, scorePoints);
            gameEnded = true;
        }
        
        // Unsubscribe to GameEnded event
        private void OnDestroy() =>  PlayerController.GameEnded -= OnGameEnded;
    }
}