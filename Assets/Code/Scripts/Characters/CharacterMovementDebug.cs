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
            DoRandomMovement();
        }

        private void DoRandomMovement()
        {
            Vector3 direction = Directions[Random.Range(0, 3)];
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, layerMask);
            if (hit)
            {
                direction *= -1;
            }

            movement.MoveInDirection(direction);
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