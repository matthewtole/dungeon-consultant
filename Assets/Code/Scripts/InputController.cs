using System.Security.Cryptography;
using Code.Scripts.Minions;
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
        [SerializeField] protected LayerMask layerMaskClick;

        [SerializeField] protected GameObject minionInfoPanel;

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
                RaycastHit2D ray = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f, layerMaskClick);
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
                        return;
                    }

                    Minion minion = ray.collider.GetComponent<Minion>();
                    if (minion)
                    {
                        if (_panel != null)
                        {
                            Destroy(_panel);
                        }
                        _panel = Instantiate(minionInfoPanel, canvas.transform);
                        _panel.GetComponent<MinionDetailsModal>().minion = minion;
                        return;
                    }

                    MinionSummonPortal portal = ray.collider.GetComponent<MinionSummonPortal>();
                    if (portal)
                    {
                        portal.OnClick();
                        return;
                    }
                    
                    Debug.Log(ray.collider.name);
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                RaycastHit2D ray = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0, layerMaskMove);
                if (ray)
                {
                    MovableItem movable = ray.collider.GetComponent<MovableItem>();
                    if (movable)
                    {
                        movable.Move();
                        movable.onMoveCancelled.AddListener(OnItemMoveFinished);
                        movable.onMoveCompleted.AddListener(OnItemMoveFinished);
                        _isMovingItem = true;
                        return;
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