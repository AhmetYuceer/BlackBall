using System;

namespace Managers
{
    public static class EventBus 
    {
        public static event Action StartGameEvent;
        public static event Action EndGameEvent;
        
        public static event Action BallJumpEvent;
        public static event Action HitTheWallEvent;
        
        public static event Action<int> UpdateCurrentScoreEvent;
        public static event Action<int> UpdateMaxScoreEvent;
        
        public static void OnStartGameEvent()
        {
            StartGameEvent?.Invoke();
        }
        
        public static void OnEndGameEvent()
        {
            EndGameEvent?.Invoke();
        }

        public static void OnBallJumpEvent()
        {
            BallJumpEvent?.Invoke();
        }

        public static void OnHitTheWallEvent()
        {
            HitTheWallEvent?.Invoke();
        }

        public static void DOOnUpdateCurrentScoreEvent(int score)
        {
            UpdateCurrentScoreEvent?.Invoke(score);
        }

        public static void DOOnUpdateMaxScoreEvent(int score)
        {
            UpdateMaxScoreEvent?.Invoke(score);
        }
 
    }
}