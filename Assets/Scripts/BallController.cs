using System;
using UnityEngine;
using Enums;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _defaultForce;
    [SerializeField] private float _secondForce;
    [SerializeField] private float _bounceForce;
    
    private Rigidbody2D _rigidbody;
    private Direction _direction;
    
    private Vector2 force = Vector2.zero;
    private Vector2 bounceForce = Vector2.zero;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        force = new Vector2(1.5f, 2f);     
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(force * _secondForce, ForceMode2D.Impulse);
        }
    }

    private void ChangeDirection(Direction direction)
    {
        _direction = direction;
        switch (_direction)
        {
            case Direction.Right:
                force = new Vector2(0.5f, 1.3f);
                break;
            case Direction.Left:
                force = new Vector2(-0.5f, 1.3f);     
                break;
        }
    }

    private void BounceForce(WallType wallType)
    {
        switch (wallType)
        {  
            case WallType.UpWall:
                bounceForce = new Vector2(0, 0);
                break;
            case WallType.DownWall:
                bounceForce = new Vector2(0, 1.3f);
                bounceForce += force;
                break;
            case WallType.LeftWall:
                bounceForce = new Vector2(1f, 0);
                break;
            case WallType.RightWall:
                bounceForce = new Vector2(-1f, 0);
                break;      
        }
        _rigidbody.AddForce(bounceForce * _bounceForce , ForceMode2D.Impulse);
    }
    
    private void OnCollisionEnter2D (Collision2D other)
    {
        if (other.transform.CompareTag("Wall") && other.transform.TryGetComponent(out Wall wall))
        {
            switch (wall.GetWallType)
            {
                case WallType.LeftWall:
                    ChangeDirection(Direction.Right);
                    break;
                case WallType.RightWall:
                    ChangeDirection(Direction.Left);
                    break;
            }
            BounceForce(wall.GetWallType);
        }
    }
}