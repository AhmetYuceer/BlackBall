using System.Collections;
using UnityEngine;
using EventBus = Managers.EventBus;

public class CameraController : MonoBehaviour
{
    private Camera _camera;

    public float duration = 0.5f;
    public float magnitude = 0.2f;
    private Vector3 originalPosition;

    private void OnEnable()
    {
        EventBus.HitTheWallEvent += EventBusOnHitTheWallEvent;
    }
    
    private void OnDisable()
    {
        EventBus.HitTheWallEvent -= EventBusOnHitTheWallEvent;
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
    
    private IEnumerator Shake()
    {
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            float xOffset = Random.Range(-0.25f, 0.25f) * magnitude;
            transform.position = new Vector3(originalPosition.x + xOffset, originalPosition.y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }
}