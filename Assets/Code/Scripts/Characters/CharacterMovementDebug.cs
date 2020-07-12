using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonManager {
    public class CharacterMovementDebug : MonoBehaviour {
        [SerializeField]
        protected CharacterMovement movement;

        static Vector3[] DIRECTIONS = { Vector3.left, Vector3.up, Vector3.down, Vector3.right };

        void Start() {
            Invoke("DoRandomMovement", 1);
        }

        protected void DoRandomMovement() {
            movement.MoveInDirection(DIRECTIONS[Random.Range(0, 3)]);
        }

        public void HandleVelocityChange() {
            if (movement.Velocity == 0) {
                Invoke("DoRandomMovement", 1);
            }
        }
    }
}