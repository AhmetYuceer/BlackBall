using Enums;
using Managers;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool _isStartedGame;
    
    [SerializeField] private float _tapJumpForce = 5f;
    [SerializeField] private float _wallBounceForce = 3f;
    
    private Rigidbody2D _rigidbody;
    private Direction _direction;

    [Header("Wall Bounce Settings")] 
    [SerializeField] private Vector2 _wallBounceDirectionUP = new Vector2(0, 0);
    [SerializeField] private Vector2 _wallBounceDirectionDown = new Vector2(0, 1.3f);
    [SerializeField] private Vector2 _wallBounceDirectionRight = new Vector2(-1f, 0);
    [SerializeField] private Vector2 _wallBounceDirectionLeft = new Vector2(1f, 0);
    private Vector2 _wallBounceDirection = Vector2.zero;
    
    [Header("Tap Bounce Settings")]
    [SerializeField] private Vector2 _tapBounceDirectionRight = new Vector2(0.5f, 1.3f);
    [SerializeField] private Vector2 _tapBounceDirectionLeft = new Vector2(-0.5f, 1.3f);
    private Vector2 _tapBounceDirection = Vector2.zero;
     
    [Header("Waiting Bounce Settings")]
    [SerializeField] private Vector2 _waitingWallBounceDirectionDown = new Vector2(0, 4.3f);
    
    private void OnEnable()
    {
        EventBus.StartGameEvent += EventBusOnStartGameEvent;
        EventBus.EndGameEvent += EventBusOnEndGameEvent;
        EventBus.BallJumpEvent += HandleJump;
    }

    private void OnDisable()
    {
        EventBus.StartGameEvent -= EventBusOnStartGameEvent;
        EventBus.EndGameEvent -= EventBusOnEndGameEvent;
        EventBus.BallJumpEvent -= HandleJump;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        ChangeDirection(Direction.Right);
    }
    
    private void HandleJump()
    {
        if (!_isStartedGame) return;
         
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(_tapBounceDirection * _tapJumpForce, ForceMode2D.Impulse);
    }
    
    private void ChangeDirection(Direction direction)
    {
        _direction = direction;
        switch (_direction)
        {
            case Direction.Right:
                _tapBounceDirection = _tapBounceDirectionRight;
                break;
            case Direction.Left:
                _tapBounceDirection = _tapBounceDirectionLeft;
                break;
        }
    }

    private void WallBounceForce(WallType wallType)
    {
        switch (wallType)
        {  
            case WallType.UpWall:
                _wallBounceDirection = _wallBounceDirectionUP;
                break;
            case WallType.DownWall:
                _wallBounceDirection = _wallBounceDirectionDown;
                _wallBounceDirection += _tapBounceDirection;
                break;
            case WallType.LeftWall:
                _wallBounceDirection = _wallBounceDirectionLeft;
                break;
            case WallType.RightWall:
                _wallBounceDirection = _wallBounceDirectionRight;
                break;      
        }
        _rigidbody.AddForce(_wallBounceDirection * _wallBounceForce , ForceMode2D.Impulse);
    }

    private void WaitingBounceForce()
    {
        _wallBounceDirection = _waitingWallBounceDirectionDown;
        _rigidbody.AddForce(_wallBounceDirection * _wallBounceForce , ForceMode2D.Impulse);
    }
    
    private void OnCollisionEnter2D (Collision2D other)
    {
        if (other.transform.CompareTag("Wall") && other.transform.TryGetComponent(out Wall wall))
        {
            if (!_isStartedGame)
            {
                WaitingBounceForce();
                return;
            }
            
            switch (wall.GetWallType)
            {
                case WallType.LeftWall:
                    ChangeDirection(Direction.Right);
                    break;
                case WallType.RightWall:
                    ChangeDirection(Direction.Left);
                    break;
            }
            WallBounceForce(wall.GetWallType);
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