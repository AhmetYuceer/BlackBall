using UnityEngine;
using Managers;

public class SoundManager : MonoBehaviour
{
   [SerializeField] private AudioClip _hitToWallClip;
   [SerializeField] private AudioClip _jumpClip;
   [SerializeField] private AudioClip _endClip;
   
   private AudioSource _audioSource;

   private void OnEnable()
   {
      EventBus.EndGameEvent += EventBusOnEndGameEvent;
      EventBus.HitTheWallEvent += EventBusOnHitTheWallEvent;
      EventBus.BallJumpEvent += EventBusOnBallJumpEvent;
   }

   private void OnDisable()
   {
      EventBus.HitTheWallEvent -= EventBusOnHitTheWallEvent;
      EventBus.BallJumpEvent -= EventBusOnBallJumpEvent;
      EventBus.EndGameEvent -= EventBusOnEndGameEvent;
   }

   private void Start()
   {
      _audioSource = GetComponent<AudioSource>();
   }
   
   private void EventBusOnBallJumpEvent()
   {
      _audioSource.PlayOneShot(_jumpClip);
   }

   private void EventBusOnHitTheWallEvent()
   {
      _audioSource.PlayOneShot(_hitToWallClip);
   }

   private void EventBusOnEndGameEvent()
   {
      _audioSource.PlayOneShot(_endClip);
   }
}
