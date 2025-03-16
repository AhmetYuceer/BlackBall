using System.Collections;
using UnityEngine;
using EventBus = Managers.EventBus;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        private Camera _camera;
        
        private bool _endGame = false;

        private float duration = 0.1f;
        private float magnitude = 0.5f;
        private Vector3 originalPosition;

        private void OnEnable()
        {
            EventBus.HitTheWallEvent += EventBusOnHitTheWallEvent;
            EventBus.StartGameEvent += EventBusOnStartGameEvent;
            EventBus.EndGameEvent += EventBusOnEndGameEvent;
        }

        private void OnDisable()
        {
            EventBus.HitTheWallEvent -= EventBusOnHitTheWallEvent;
            EventBus.StartGameEvent-= EventBusOnStartGameEvent;
            EventBus.EndGameEvent -= EventBusOnEndGameEvent;
        }

        private void Start()
        {
            _camera = GetComponent<Camera>();
            originalPosition = transform.position;
        }

        private void EventBusOnHitTheWallEvent()
        {
            StartCoroutine(Shake());
        }
    
        private void EventBusOnStartGameEvent()
        {
            _endGame = false;
        }
        
        private void EventBusOnEndGameEvent()
        {
            _endGame = true;
        }
        
        private IEnumerator Shake()
        {
            float elapsed = 0f;
        
            while (elapsed < duration && !_endGame)
            {
                float xOffset = Random.Range(-0.25f, 0.25f) * magnitude;
                transform.position = new Vector3(originalPosition.x + xOffset, originalPosition.y, originalPosition.z);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.position = originalPosition;
        }
    }
}