using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Items.ShopEntries;
using UnityEngine;

public class ItemBuilder : MonoBehaviour
{
    private ShopEntry _shopEntry;
    private GameObject _buildObject;
    private Camera _camera;
    private ContactFilter2D _contactFilter;
    private ContactFilter2D _contactFilterNoBuild;
    private BoxCollider2D _boxCollider2D;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _camera = Camera.main;
    }

    public void Init(ShopEntry entry)
    {
        _shopEntry = entry;
        _buildObject = Instantiate(_shopEntry.prefab, transform);
        _buildObject.transform.position = Vector3.zero;
        //_boxCollider2D.size = _buildObject.GetComponent<BoxCollider2D>().size;
        _contactFilter = new ContactFilter2D {layerMask = _shopEntry.buildLayers, useLayerMask = true, useTriggers = true};
        _contactFilterNoBuild = new ContactFilter2D {layerMask = _shopEntry.noBuildLayers, useLayerMask = true, useTriggers = true};
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && _buildObject)
        {
            Collider2D[] colliders = new Collider2D[1];
            bool canBuild =  _boxCollider2D.GetContacts(_contactFilter, colliders) > 0 && _boxCollider2D.GetContacts(_contactFilterNoBuild, colliders) == 0;


            if (canBuild)
            {
                _buildObject.transform.parent = transform.parent;
                _buildObject.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3Int.RoundToInt(_camera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10f));
    }
}
