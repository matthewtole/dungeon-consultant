using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildController : MonoBehaviour
{
    [SerializeField] protected Tilemap buildTilemap;
    [SerializeField] protected Tilemap wallTilemap;
    [SerializeField] protected TileBase buildTile;
    [SerializeField] protected TileBase buildErrorTile;
    [SerializeField] protected TileBase wallTile;
    private readonly Vector3 _offset = new Vector3(-0.5f, -0.5f);
    private Camera _camera;

    private bool _isDragging;
    private bool _isValidBuild;
    private Vector3Int _start;
    private Vector3Int _end;

    private void Start()
    {
        _camera = Camera.main;
    }

    private Vector3Int TilePositionFromMouse()
    {
        return Vector3Int.RoundToInt(_camera.ScreenToWorldPoint(Input.mousePosition) + _offset);
    }

    private void Update()
    {
        if (!_isDragging && Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _start = TilePositionFromMouse();
        }

        if (_isDragging)
        {
            if (!_end.Equals(TilePositionFromMouse()))
            {
                _end = TilePositionFromMouse();
                UpdateBuildValidity();
                UpdateBuildTilemap();
            }
        }

        if (_isDragging && Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            ClearBuildTilemap();
            if (_isValidBuild)
            {
                BuildWalls();
            }

            _start = _end = Vector3Int.zero;
        }
        
    }

    private void UpdateBuildValidity()
    {
        _isValidBuild = true;
        for (int i = Math.Min(_start.x, _end.x); i <= Math.Max(_start.x, _end.x); i++)
        {
            for (int j = Math.Min(_start.y, _end.y); j <= Math.Max(_start.y, _end.y); j++)
            {
                if (wallTilemap.HasTile(new Vector3Int(i, j, 0)))
                {
                    _isValidBuild = false;
                    return;
                }
            }
            
        }
    }

    private void BuildWalls()
    {
        for (int i = Math.Min(_start.x, _end.x); i <= Math.Max(_start.x, _end.x); i++)
        {
            wallTilemap.SetTile(new Vector3Int(i,_start.y,0), wallTile);
            wallTilemap.SetTile(new Vector3Int(i,_end.y,0), wallTile);
        }
        
        for (int i = Math.Min(_start.y, _end.y); i <= Math.Max(_start.y, _end.y); i++)
        {
            wallTilemap.SetTile(new Vector3Int(_start.x, i,0), wallTile);
            wallTilemap.SetTile(new Vector3Int(_end.x,i, 0), wallTile);
        }
    }

    private void ClearBuildTilemap()
    {
        buildTilemap.ClearAllTiles();
    }

    private void UpdateBuildTilemap()
    {
        ClearBuildTilemap();
        for (int i = Math.Min(_start.x, _end.x); i <= Math.Max(_start.x, _end.x); i++)
        {
            for (int j = Math.Min(_start.y, _end.y); j <= Math.Max(_start.y, _end.y); j++)
            {
                buildTilemap.SetTile(new Vector3Int(i,j,0), _isValidBuild ? buildTile : buildErrorTile);
            }
            
        }
    }
}
