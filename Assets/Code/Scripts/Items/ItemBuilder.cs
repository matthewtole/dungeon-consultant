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

    private float _buildTimeout;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _camera = Camera.main;
        _buildTimeout = Time.time + 0.5f;
    }

    public void Init(ShopEntry entry)
    {
        _shopEntry = entry;
        _buildObject = Instantiate(_shopEntry.prefab, transform);
        _buildObject.transform.position = Vector3.zero;
        _boxCollider2D.size = _buildObject.GetComponent<BoxCollider2D>().size;
        _contactFilter = new ContactFilter2D {layerMask = _shopEntry.buildLayers, useLayerMask = true, useTriggers = true};
        _contactFilterNoBuild = new ContactFilter2D {layerMask = _shopEntry.noBuildLayers, useLayerMask = true, useTriggers = true};
    }


    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonUp(0) || !_buildObject || !(Time.time > _buildTimeout))
        {
            return;
        }

        Collider2D[] colliders = new Collider2D[1];
        bool canBuild =  _boxCollider2D.GetContacts(_contactFilter, colliders) > 0 && _boxCollider2D.GetContacts(_contactFilterNoBuild, colliders) == 0;

        if (!canBuild)
        {
            return;
        }

        _buildObject.transform.parent = transform.parent;
        _buildObject.SetActive(true);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3Int.RoundToInt(_camera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10f));
    }
}
