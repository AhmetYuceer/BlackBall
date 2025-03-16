using Managers;
using UnityEngine;

namespace Controllers
{
    public class AnimationController : MonoBehaviour
    {
         private BallController _ball;
        [SerializeField] private GameObject _jumpAnimationPrefab;
        private bool _isStartedGame = false;
        
        private void OnEnable()
        {
            EventBus.StartGameEvent += EventBusOnStartGameEvent;
            EventBus.EndGameEvent += EventBusOnEndGameEvent;
            EventBus.BallJumpEvent += EventBusOnBallJumpEvent;
        }

        private void OnDisable()
        {
            EventBus.StartGameEvent -= EventBusOnStartGameEvent;
            EventBus.EndGameEvent -= EventBusOnEndGameEvent;
            EventBus.BallJumpEvent -= EventBusOnBallJumpEvent;
        }
        
        private void EventBusOnStartGameEvent()
        {
            _isStartedGame = true;
        }
        
        private void EventBusOnEndGameEvent()
        {
            _isStartedGame = false;
        }

        private void Start()
        {
            _ball = GetComponent<BallController>();
        }
        
        private void EventBusOnBallJumpEvent()
        {
            if (_isStartedGame)
                Instantiate(_jumpAnimationPrefab, _ball.transform.position, Quaternion.identity);
        }
    }
}