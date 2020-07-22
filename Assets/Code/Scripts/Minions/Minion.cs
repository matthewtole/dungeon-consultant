using System;
using Code.Scripts.Characters;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Code.Scripts.Minions
{
    public class Minion : MonoBehaviour
    {
        public string characterName;
        private Room _currentRoom = null;

        private float _attackTimeout;
        private Transform _attackTarget;
        private bool _pickedUp = false;
        private bool _isMoving = false;

        [FormerlySerializedAs("animation")] [SerializeField] protected CharacterAnimation characterAnimation;
        [SerializeField] protected CharacterPathfinding characterPathfinding;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Room room = other.GetComponent<Room>();
            if (room)
            {
                transform.parent = room.transform;
                _currentRoom = room;
                if (!_pickedUp)
                {
                    UpdateCurrentObjective();
                }
            }
        }

        private void UpdateCurrentObjective()
        {
            if (_currentRoom == null)
            {
                return;
            }
            
            _attackTarget = null;

            switch (_currentRoom.type)
            {
                case RoomType.General:
                    break;
                case RoomType.Training:
                    TrainingTarget[] targets = _currentRoom.GetComponentsInChildren<TrainingTarget>();
                    TrainingTarget[] freeTargets = Array.FindAll(targets, t => t.currentMinion == null);
                    if (freeTargets.Length >= 1)
                    {
                        TrainingTarget target = freeTargets[0];
                        target.currentMinion = this;
                        _attackTarget = target.transform;
                        characterPathfinding.SetDestination(target.target.transform);
                        _isMoving = true;
                    }
                    else
                    {
                        Invoke(nameof(UpdateCurrentObjective), 3f);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Room room = other.GetComponent<Room>();
            if (room)
            {
                transform.parent = null;
                CancelCurrentObjective();
                _currentRoom = null;
            }
        }

        private void Update()
        {
            if (_pickedUp)
            {
                return;
            }

            if (!_isMoving && _attackTarget)
            {
                Attack();
            }
        }

        private void Attack()
        {
            if (Time.time > _attackTimeout)
            {
                characterAnimation.Attack(_attackTarget.position);
                _attackTimeout = Time.time + Random.Range(2f, 4f);
            }
        }

        public void OnPickup()
        {
            CancelCurrentObjective();
            characterAnimation.Stop();
            characterPathfinding.Stop();
            _pickedUp = true;
        }

        private void CancelCurrentObjective()
        {
            switch (_currentRoom.type)
            {
                case RoomType.General:
                    break;
                case RoomType.Training:
                    if (_attackTarget != null)
                    {
                        TrainingTarget target = _attackTarget.GetComponent<TrainingTarget>();
                        target.currentMinion = null;
                        _attackTarget = null;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnDropOff()
        {
            _attackTimeout = Time.time + Random.Range(1f, 3f);
            _pickedUp = false;
            UpdateCurrentObjective();
        }

        public void HandlePathfindingComplete()
        {
            _isMoving = false;
        }
    }
}