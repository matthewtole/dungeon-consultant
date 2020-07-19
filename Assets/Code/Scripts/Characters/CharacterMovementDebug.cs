using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.Characters
{
    public class CharacterMovementDebug : MonoBehaviour
    {
        [SerializeField] protected CharacterMovement movement;
        [SerializeField] protected LayerMask layerMask;

        static readonly Vector3[] Directions = {Vector3.left, Vector3.up, Vector3.down, Vector3.right};

        void Start()
        {
            Invoke(nameof(DoRandomMovement), 1f);
        }

        private void DoRandomMovement()
        {
            int direction = Random.Range(0, 3);
            int directionsTried = 0;
            while (directionsTried < 4)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Directions[direction], 1f, layerMask);
                if (hit)
                {
                    direction = (direction + 1) % 4;
                    directionsTried += 1;
                }
                else
                {
                    movement.MoveInDirection(Directions[direction]);
                    break;
                } 
            }
        }

        public void HandleVelocityChange()
        {
            if (Math.Abs(movement.Velocity) < 0.001f)
            {
                DoRandomMovement();
            }
        }
    }
}