using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TouchMovement : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private MainCharacterMovement _mainCharacter;

    private bool _isDragging = false;
    private float _offsetX;

    [Inject]
    private void Constructor(MainCharacterMovement mainCharacter)
    {
        _mainCharacter = mainCharacter;
    }

    private void Awake()
    {
        if (_mainCharacter == null)
        {
            Debug.LogError("PlayerMovement component not found on the main character!");
        }
    }

    private void Update()
    {
        if (!_isDragging)
        {
            _mainCharacter.UpdateMovement();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging)
        {
            Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(eventData.position);
            _offsetX = touchWorldPos.x - _mainCharacter.gameObject.transform.position.x;
            _isDragging = true;
        }

        Vector3 currentTouchPos = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector3 targetPosition = new Vector3(currentTouchPos.x - _offsetX, _mainCharacter.gameObject.transform.position.y,
             _mainCharacter.gameObject.transform.position.z);

        _mainCharacter.MoveToPosition(targetPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
    }
}

