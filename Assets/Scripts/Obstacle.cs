using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    public void Activate()
    {
        gameObject.SetActive(true);
        transform.DOShakePosition(0.1f, 0.1f, 1, 10, false, true);
    }
    
    public void Deactivate()
    {
        transform.DOShakePosition(0.1f, 0.1f, 1, 10, false, true)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}