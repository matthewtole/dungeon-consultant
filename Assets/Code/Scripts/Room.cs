using System;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Code.Scripts
{
    public enum RoomType
    {
        General,
        Training
    }
    public class Room : MonoBehaviour
    {
        public RoomType type;

        [SerializeField] protected Tilemap tilemapWalls;
        [SerializeField] protected Tilemap tilemapWallDecorations;
        [SerializeField] protected TileBase warningTape;
        [SerializeField] protected BoxCollider2D boxCollider;

        private List<Vector3Int> _wallsToDecorate = new List<Vector3Int>();
        void Start()
        {
            
            CalculateWallsToDecorate();
            UpdateWarningTape();
        }

        private void CalculateWallsToDecorate()
        {
            _wallsToDecorate = new List<Vector3Int>();
            
            for (float y = boxCollider.bounds.min.y; y < boxCollider.bounds.max.y; y += 1)
            {
                for (float x = boxCollider.bounds.min.x; x < boxCollider.bounds.max.x; x += 1)
                {
                    Vector3Int pos = Vector3Int.FloorToInt(new Vector3(x, y, 0));
                    if (tilemapWalls.GetTile(pos) && !tilemapWalls.GetTile(pos + Vector3Int.down))
                    {
                        _wallsToDecorate.Add(pos);
                    }
                }
            }
        }
        
        private void UpdateWarningTape()
        {
            bool valid = MeetsRoomRequirements();
            _wallsToDecorate.ForEach(pos => tilemapWallDecorations.SetTile(pos, valid ? null : warningTape));
        }

        private void OnTransformChildrenChanged()
        {
            UpdateWarningTape();
        }

        private bool MeetsRoomRequirements()
        {
            switch (type)
            {
                case RoomType.General:
                    return true;
                case RoomType.Training:
                    return transform.GetComponentsInChildren<TrainingTarget>().Length > 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Bounds GetBounds()
        {
            return boxCollider.bounds;
        }

        [Button]
        public void CheckRequirements()
        {
            CalculateWallsToDecorate();
            UpdateWarningTape();
        }

        [Button]
        public void ClearTape()
        {
            _wallsToDecorate.ForEach(pos => tilemapWallDecorations.SetTile(pos, null));
        }
    }
}