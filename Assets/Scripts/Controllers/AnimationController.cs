using Managers;
using UnityEngine;

namespace Controllers
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private BallController _ball;
        [SerializeField] private GameObject _jumpAnimationPrefab;

        private void OnEnable()
        {
            EventBus.BallJumpEvent += EventBusOnBallJumpEvent;
        }

        private void OnDisable()
        {
            EventBus.BallJumpEvent -= EventBusOnBallJumpEvent;
        }

        private void EventBusOnBallJumpEvent()
        {
            Instantiate(_jumpAnimationPrefab, _ball.transform.position, Quaternion.identity);
        }
    }
}