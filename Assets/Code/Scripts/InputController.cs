using Code.Scripts.Raiders;
using Code.Scripts.UI;
using UnityEngine;

namespace Code.Scripts
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] protected GameObject raiderInfoPanel;
        [SerializeField] protected Canvas canvas;

        private GameObject _panel = null;
        private Camera _camera;

        // Start is called before the first frame update
        private void Awake()
        {
            _camera = Camera.main;
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
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
        }
    }
}