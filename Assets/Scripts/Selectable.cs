using System.Collections;
using System.Collections.Generic;
using MyBox;
using UI;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] protected SelectionBoxController selectionBoxPrefab;
    [SerializeField] protected BoxCollider2D collider;
    private SelectionBoxController _selectionBox;
    private bool _isSelected = false;

    [ButtonMethod()]
    public void OnSelect()
    {
        if (_isSelected)
        {
            return;
            
        }

        _isSelected = true;
        _selectionBox = Instantiate(selectionBoxPrefab, transform);
        _selectionBox.SetSize(collider.size);
        _selectionBox.transform.localPosition = collider.offset;
    }

    [ButtonMethod()]
    public void OnDeselect()
    {
        if (!_isSelected)
        {
            return;
        }

        _isSelected = false;
        DestroyImmediate(_selectionBox.gameObject);
    }
}
