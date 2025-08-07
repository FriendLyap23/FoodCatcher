using UnityEngine;
using Zenject;

public class ScreenLimitation : MonoBehaviour
{
    private MainCharacterMovement _mainCharacter;
    private float _playerWidth;
    private Camera _mainCamera;

    [Inject]
    private void Constructor(MainCharacterMovement mainCharacter)
    {
        _mainCharacter = mainCharacter;
    }

    private void Start()
    {
        _mainCamera = Camera.main;

        SpriteRenderer sprite = _mainCharacter.GetComponent<SpriteRenderer>();
        _playerWidth = sprite.bounds.size.x / 2f;
    }

    public Vector3 GetClampedPosition(Vector3 targetPosition)
    {
        Vector3 minScreenBounds = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = _mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0));

        float clampedX = Mathf.Clamp(targetPosition.x, minScreenBounds.x + _playerWidth, 
            maxScreenBounds.x - _playerWidth);

        return new Vector3(clampedX, targetPosition.y, targetPosition.z);
    }

    public bool IsOutOfBounds(Vector3 position, out float correctionForce) 
    {
        correctionForce = 0f;
        Vector3 clamped = GetClampedPosition(position);
        bool isOut = position != clamped;

        if (isOut)
            correctionForce = (clamped.x - position.x) * 0.2f;

        return isOut;
    }
}
