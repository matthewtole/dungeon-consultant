using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    private Selectable _selectable;
    private Camera _camera;

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
            RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && hit.transform && hit.transform.GetComponent<Selectable>())
            {
                _selectable = hit.transform.GetComponent<Selectable>();
            }
            else
            {
                if (_selectable != null)
                {
                    _selectable.OnDeselect();
                }
                _selectable = null;
            }
        }

        if (_selectable != null && Input.GetMouseButtonUp(0))
        {
            _selectable.OnSelect();
        }
    }
}
