using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _maxScoreText;
        [SerializeField] private TextMeshPro _currentScoreText;

        [SerializeField] private GameObject _endGamePanel;
        [SerializeField] private TextMeshProUGUI _currentScoreTextGameOver;
        [SerializeField] private TextMeshProUGUI _maxScoreTextGameOver;
        [SerializeField] private Button _tryAgainButton;
        
        private void OnEnable()
        {
            EventBus.StartGameEvent += EventBusOnStartGameEvent;
            EventBus.EndGameEvent += EventBusOnEndGameEvent;
            EventBus.UpdateCurrentScoreEvent += UpdateCurrentScoreText;
            EventBus.UpdateMaxScoreEvent += UpdateMaxScoreText;
        }

        private void OnDisable()
        {
            EventBus.UpdateCurrentScoreEvent -= UpdateCurrentScoreText;
            EventBus.UpdateMaxScoreEvent -= UpdateMaxScoreText;
            EventBus.StartGameEvent -= EventBusOnStartGameEvent;
            EventBus.EndGameEvent -= EventBusOnEndGameEvent;
        }

        private void Start()
        {
            _tryAgainButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }

        private void EventBusOnStartGameEvent()
        {
            _endGamePanel.SetActive(false);
        }

        private void EventBusOnEndGameEvent()
        {
            _endGamePanel.SetActive(true);
        }
        
        private void UpdateMaxScoreText(int score)
        {
            _maxScoreText.text = score.ToString();
            _currentScoreTextGameOver.text = score.ToString();
        }
        
        private void UpdateCurrentScoreText(int score)
        {
            _currentScoreText.text = score.ToString();
            _maxScoreTextGameOver.text = score.ToString();
        }
    }
}