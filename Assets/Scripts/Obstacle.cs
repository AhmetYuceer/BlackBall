using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _collider;
    [SerializeField] private Animator _animator;
    
    public void Activate()
    {
        if (_animator == null )
            _animator = GetComponent<Animator>();
        
        gameObject.SetActive(true);
        _animator.SetTrigger("Active");
        // transform.DOShakePosition(0.1f, 0.1f, 1, 10, false, true);
    }
    
    public void Deactivate()
    {
        _animator.SetTrigger("Deactive");
        
        // transform.DOShakePosition(0.1f, 0.1f, 1, 10, false, true)
        //     .OnComplete(() =>
        //     {
        //         gameObject.SetActive(false);
        //     });
    }

    private void SetActiveForAnimation()
    {
        gameObject.SetActive(false);
    }
    
    public void ColliderTrigger(bool isTrigger)
    {
        _collider.isTrigger = isTrigger;
    }
}