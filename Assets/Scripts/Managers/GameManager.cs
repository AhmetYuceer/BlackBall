using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private bool _isStartedGame = false;
        
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
            _isStartedGame = false;
        }

        private void StartGame()
        {
            _isStartedGame = true;
            Debug.Log("Game started");
        }

        private void EndGame()
        {
            _isStartedGame = false;
            Debug.Log("Game ended");
        }
    }
}