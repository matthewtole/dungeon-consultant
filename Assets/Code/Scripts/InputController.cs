using System.Collections;
using Code.Scripts.Raiders;
using Code.Scripts.UI;
using UnityEngine;

namespace Code.Scripts
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] protected GameObject raiderInfoPanel;
        [SerializeField] protected Canvas canvas;
        [SerializeField] protected LayerMask layerMaskMove;

        private GameObject _panel = null;
        private Camera _camera;

        private bool _isMovingItem = false;

        private void Awake()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            if (_isMovingItem)
            {
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit2D ray = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (ray)
                {
                    Raider raider = ray.collider.GetComponent<Raider>();
                    if (raider)
                    {
                        if (_panel != null)
                        {
                            Destroy(_panel);
                        }

                        _panel = Instantiate(raiderInfoPanel, canvas.transform);
                        _panel.GetComponent<RaiderInfoPanel>().raider = raider;
                    }
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                RaycastHit2D ray = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0, layerMaskMove);
                if (ray)
                {
                    MoveableItem moveable = ray.collider.GetComponent<MoveableItem>();
                    if (moveable)
                    {
                        moveable.Move();
                        moveable.onMoveCancelled.AddListener(OnItemMoveFinished);
                        moveable.onMoveCompleted.AddListener(OnItemMoveFinished);
                        _isMovingItem = true;
                    }
                }
            }
        }

        private void OnItemMoveFinished()
        {
            Invoke(nameof(EndMove), 0.1f);
        }

        private void EndMove()
        {
            _isMovingItem = false;
        }
    }
}