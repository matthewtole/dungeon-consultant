using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Characters;
using Code.Scripts.Minions;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CharacterPathfinding : MonoBehaviour
{
    [SerializeField] protected Seeker seeker;
    [SerializeField] protected CharacterMovement characterMovement;

    public UnityEvent onPathCompleted;
    
    private Path _currentPath;
    private int _currentPathIndex;
    private Vector3 _destination;

    private void OnPathDelegate(Path path)
    {
        if (path.error)
        {
            Debug.LogError(path.errorLog);
            return;
        }

        _currentPath = path;
        _currentPathIndex = 0;
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        if (_currentPath == null)
        {
            return;
        }

        if ((_currentPath.vectorPath[_currentPathIndex] - transform.position).magnitude == 0)
        {

            _currentPathIndex += 1;
        }

        if (_currentPathIndex >= _currentPath.vectorPath.Count)
        {
            onPathCompleted.Invoke();
            _currentPath = null;
            _currentPathIndex = 0;
            return;
        }
        
        characterMovement.MoveInDirection(_currentPath.vectorPath[_currentPathIndex] - transform.position);
    }

    public void HandleVelocityChanged()
    {

        if (Math.Abs(characterMovement.Velocity) < 0.001f)
        {
            MoveToNextPoint();
        }
    }

    public void SetDestination(Vector3 destination)
    {
        _destination = destination;
        seeker.StartPath(transform.position, _destination, OnPathDelegate);
    }

    public void Stop()
    {
        _currentPath = null;
    }
}
