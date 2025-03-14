using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        private const int _increaseAmount = 1;
        private int _maxScore = 0;
        private int _currentScore = 0;

        private void OnEnable()
        {
            EventBus.StartGameEvent += LoadScore;
            EventBus.HitTheWallEvent += AddScore;
            EventBus.EndGameEvent += SaveMaxScore;
        }

        private void OnDisable()
        {            
            EventBus.StartGameEvent -= LoadScore;
            EventBus.HitTheWallEvent -= AddScore;
            EventBus.EndGameEvent -= SaveMaxScore;
        }

        private void Start()
        {
            _currentScore = 0;
        }
        
        private void AddScore()
        {
            _currentScore += _increaseAmount;
            
            EventBus.DOOnUpdateCurrentScoreEvent(_currentScore);
            
            if (_currentScore > _maxScore)
                EventBus.DOOnUpdateMaxScoreEvent(_currentScore);
        }

        private void LoadScore()
        {
            int maxScore = SaveAndLoad.SaveAndLoad.LoadScore();
            EventBus.DOOnUpdateCurrentScoreEvent(_currentScore);
            EventBus.DOOnUpdateMaxScoreEvent(maxScore);
        }

        private void SaveMaxScore()
        {
            int maxScore = SaveAndLoad.SaveAndLoad.LoadScore();

            if (_currentScore > maxScore)
                SaveAndLoad.SaveAndLoad.SaveScore(_currentScore);
        }
    }
}