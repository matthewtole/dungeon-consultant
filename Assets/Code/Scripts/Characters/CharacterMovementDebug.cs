using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonManager {
    public class CharacterMovementDebug : MonoBehaviour {
        [SerializeField]
        protected CharacterMovement movement;
        [SerializeField]
        protected LayerMask layerMask;

        static Vector3[] DIRECTIONS = { Vector3.left, Vector3.up, Vector3.down, Vector3.right };

        void Start() {
            DoRandomMovement();
        }

        protected void DoRandomMovement() {
            Vector3 direction = DIRECTIONS[Random.Range(0, 3)];
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, layerMask);
            if (hit) {
                direction *= -1;
            }
            movement.MoveInDirection(direction);
        }

        public void HandleVelocityChange() {
            if (movement.Velocity == 0) {
                DoRandomMovement();
            }
        }
    }
}