using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _maxScoreText;
        [SerializeField] private TextMeshPro _currentScoreText;

        private void OnEnable()
        {
            EventBus.UpdateCurrentScoreEvent += UpdateCurrentScoreText;
            EventBus.UpdateMaxScoreEvent += UpdateMaxScoreText;
        }

        private void OnDisable()
        {
            EventBus.UpdateCurrentScoreEvent -= UpdateCurrentScoreText;
            EventBus.UpdateMaxScoreEvent -= UpdateMaxScoreText;
        }

        private void UpdateMaxScoreText(int score)
        {
            _maxScoreText.text = score.ToString();
        }
        
        private void UpdateCurrentScoreText(int score)
        {
            _currentScoreText.text = score.ToString();
        }
    }
}