using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct MovableLayers
{
    public Vector2Int offset;
    public LayerMask validPlacementLayers;
    public LayerMask invalidPlacementLayers;
}

public class MovableItem : MonoBehaviour
{
    private bool _isMoving = false;
    private Camera _camera;
    private SpriteRenderer _spriteRenderer;
    private int _originalLayer;
    private Vector3 _originalPosition;
    private float _moveDebounce;

    private Vector3 _lastPosition;

    [SerializeField] protected MovableLayers[] layers;
    public UnityEvent onMoveCompleted;
    public UnityEvent onMoveCancelled;
    public UnityEvent onMoveStarted;
    private readonly Color _invalidColor = new Color(255, 255, 255, 0.3f);
    private readonly Color _validColor = new Color(255, 255, 255, 0.7f);

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _camera = Camera.main;
    }

    public void Move()
    {
        _originalPosition = transform.position;
        _originalLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("UI");
        _isMoving = true;
        _moveDebounce = Time.time + 0.1f;
        
        transform.position =
            Vector3Int.RoundToInt(_camera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10f));

        UpdatePlacementDisplay();
        onMoveStarted.Invoke();
    }

    private void UpdatePlacementDisplay()
    {
        if (transform.position.Equals(_lastPosition))
        {
            return;
        }

        _lastPosition = transform.position;
        _spriteRenderer.color = IsValidPlacement() ? _validColor : _invalidColor;
    }

    private void Update()
    {
        if (_isMoving && Time.time > _moveDebounce)
        {
            if (Input.GetMouseButtonUp(0))
            {
                FinishMove();
            }

            if (Input.GetMouseButtonUp(1))
            {
                CancelMove();
            }
        }
    }

    private void CancelMove()
    {
        ResetObject();
        transform.position = _originalPosition;
        onMoveCancelled.Invoke();
    }

    private bool IsValidPlacement()
    {
        Vector3 position = transform.position;
        
        return Array.TrueForAll(layers, layer =>
        {
            Vector3 layerPosition = position + new Vector3(layer.offset.x, layer.offset.y, 0);
            RaycastHit2D validPositionHit = Physics2D.Raycast(layerPosition, Vector2.zero, 0,
                layer.validPlacementLayers);
            if (!validPositionHit)
            {
                return false;
            }

            RaycastHit2D invalidPositionHit = Physics2D.Raycast(layerPosition, Vector2.zero, 0,
                layer.invalidPlacementLayers);
            return !invalidPositionHit;
        });
    }
    
    private void FinishMove()
    {
        if (IsValidPlacement())
        {
            ResetObject();
            onMoveCompleted.Invoke();
        }
    }

    private void ResetObject()
    {
        _spriteRenderer.color = Color.white;
        _isMoving = false;
        gameObject.layer = _originalLayer;

    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            transform.position =
                Vector3Int.RoundToInt(_camera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10f));
            UpdatePlacementDisplay();
        }
    }
}
