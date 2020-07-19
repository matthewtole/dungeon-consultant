using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[Serializable]
public struct MoveableLayers
{
    public Vector2Int offset;
    public LayerMask canBuild;
    public LayerMask cannotBuild;
}

public class MoveableItem : MonoBehaviour
{
    private bool _isMoving = false;
    private Camera _camera;
    private SpriteRenderer _spriteRenderer;
    private int _originalLayer;
    private Vector3 _originalPosition;
    private float _moveDebounce;

    [SerializeField] protected MoveableLayers[] layers;
    public UnityEvent onMoveCompleted;
    public UnityEvent onMoveCancelled;
    public UnityEvent onMoveStarted;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _camera = Camera.main;
    }

    public void Move()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 0.5f);
        _originalPosition = transform.position;
        _originalLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("UI");
        onMoveStarted.Invoke();
        _isMoving = true;
        _moveDebounce = Time.time + 0.1f;
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

    private bool CanBuild()
    {
        Vector3 position = transform.position;
        
        return Array.TrueForAll(layers, layer =>
        {
            Vector3 layerPosition = position + new Vector3(layer.offset.x, layer.offset.y, 0);
            RaycastHit2D buildHit = Physics2D.Raycast(layerPosition, Vector2.zero, 0,
                layer.canBuild);
            if (!buildHit)
            {
                return false;
            }

            RaycastHit2D noBuildHit = Physics2D.Raycast(layerPosition, Vector2.zero, 0,
                layer.cannotBuild);
            return !noBuildHit;
        });
    }
    
    private void FinishMove()
    {
        if (CanBuild())
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
        }
    }
}
