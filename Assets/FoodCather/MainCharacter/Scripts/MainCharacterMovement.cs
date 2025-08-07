using UnityEngine;
using Zenject;

public class MainCharacterMovement : MonoBehaviour
{
    [SerializeField] private float _deceleration = 3f;
    [SerializeField] private float _maxSpeed = 10f;

    private float _currentVelocity;
    private Transform _transform;

    private ScreenLimitation _screenLimitation;

    [Inject]
    private void Constructor(ScreenLimitation screenLimitation)
    {
        _screenLimitation = screenLimitation;
    }

    private void Awake()
    {
        _transform = transform;
    }

    public void MoveToPosition(Vector3 targetPosition)
    {
        targetPosition = _screenLimitation.GetClampedPosition(targetPosition);
        _currentVelocity = (targetPosition.x - _transform.position.x) / Time.deltaTime;
        _currentVelocity = Mathf.Clamp(_currentVelocity, -_maxSpeed, _maxSpeed);
        _transform.position = targetPosition;
    }

    public void ApplyVelocity(float velocity)
    {
        _currentVelocity = Mathf.Clamp(velocity, -_maxSpeed, _maxSpeed);
    }

    public void UpdateMovement()
    {
        if (Mathf.Abs(_currentVelocity) > 0.1f)
        {
            _currentVelocity = Mathf.Lerp(_currentVelocity, 0, _deceleration * Time.deltaTime);

            Vector3 newPosition = _transform.position + new Vector3(_currentVelocity * Time.deltaTime, 0, 0);

            if (_screenLimitation.IsOutOfBounds(newPosition, out float correction))
            {
                _currentVelocity += correction;
                newPosition = _screenLimitation.GetClampedPosition(newPosition);
            }

            _transform.position = newPosition;
        }
        else
        {
            _currentVelocity = 0;
        }
    }
}
