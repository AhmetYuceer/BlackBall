using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private bool _isStartedGame = false;

        private void OnEnable()
        {
            EventBus.StartGameEvent += EventBusOnStartGameEvent;
            EventBus.EndGameEvent += EventBusOnEndGameEvent;
        }

        private void OnDisable()
        {
            EventBus.StartGameEvent -= EventBusOnStartGameEvent;
            EventBus.EndGameEvent -= EventBusOnEndGameEvent;
        }

        private void Start()
        {
            _isStartedGame = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!_isStartedGame)
                    EventBus.OnStartGameEvent();
             
                EventBus.OnBallJumpEvent();
            }
        }
        
        private void EventBusOnStartGameEvent()
        {
            _isStartedGame = true;
        }
        
        private void EventBusOnEndGameEvent()
        {
            _isStartedGame = false;
        }
    }
}