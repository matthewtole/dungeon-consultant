using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject _selectable;
    private Camera _camera;
    [SerializeField] protected GameObjectValueList selectableValueList;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && hit.transform && hit.transform.GetComponent<Selectable>())
            {
                _selectable = hit.transform.gameObject;
            }
            else
            {
                while (selectableValueList.Count > 0)
                {
                    selectableValueList.RemoveAt(0);
                }
                _selectable = null;
            }
        }

        if (_selectable != null && Input.GetMouseButtonUp(0))
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                while (selectableValueList.Count > 0)
                {
                    selectableValueList.RemoveAt(0);
                }
            }

            selectableValueList.Add(_selectable);
        }
    }
}
