using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DungeonManager {
    public class CharacterMovement : MonoBehaviour {
        [SerializeField]
        protected float speed = 1f;
        [SerializeField]
        protected UnityEvent onVelocityChange;
        [SerializeField]
        protected UnityEvent onDirectionChange;

        protected float velocity = 0f;
        protected Vector2 direction = Vector2.zero;
        protected Vector3 target;

        public float Velocity {
            get { return velocity; }
        }

        public Vector2 Direction {
            get { return direction; }
        }

        public void MoveInDirection(Vector3 direction) {
            if (velocity > 0) { return; }
            target = gameObject.transform.position + direction.normalized;
            velocity = speed;
            this.direction = direction;
            onVelocityChange.Invoke();
            onDirectionChange.Invoke();
        }

        private void Update() {
            if (velocity > 0 && (gameObject.transform.position - target).magnitude < 0.01f) {
                gameObject.transform.position = target;
                velocity = 0;
                onVelocityChange.Invoke();
            }
        }

        void FixedUpdate() {
            if (velocity > 0) {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, Time.deltaTime * speed);
            }
        }
    }

}