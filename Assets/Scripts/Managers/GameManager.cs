using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            EventBus.StartGameEvent += StartGame;
            EventBus.EndGameEvent += EndGame;
        }

        private void OnDisable()
        {
            EventBus.StartGameEvent -= StartGame;
            EventBus.EndGameEvent -= EndGame;
        }

        private void Start()
        {
            Time.timeScale = 1f;
        }

        private void StartGame()
        {
            Debug.Log("Game started");
        }

        private void EndGame()
        {
            Time.timeScale = 0f;
            Debug.Log("Game Ended");
        }
    }
}