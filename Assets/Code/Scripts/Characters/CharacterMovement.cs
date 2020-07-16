using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] protected float speed = 1f;
        [SerializeField] protected UnityEvent onVelocityChange;
        [SerializeField] protected UnityEvent onDirectionChange;

        private Vector3 _target;

        private float _stunTimeout = 0f;

        public float Velocity { get; private set; } = 0f;
        public Vector2 Direction { get; private set; } = Vector2.zero;

        public void MoveInDirection(Vector3 moveDirection)
        {
            if (_stunTimeout > 0)
            {
                return;
            }

            if (Velocity > 0)
            {
                return;
            }

            _target = gameObject.transform.position + moveDirection.normalized;
            Velocity = speed;
            Direction = moveDirection;
            onVelocityChange.Invoke();
            onDirectionChange.Invoke();
        }

        public void Stun(float duration)
        {
            _stunTimeout = Time.time + duration;
            Velocity = 0;
            Direction = Vector3.zero;
            _target = gameObject.transform.position;
            onVelocityChange.Invoke();
        }

        private void Update()
        {
            if (Velocity > 0 && (gameObject.transform.position - _target).magnitude < 0.01f)
            {
                gameObject.transform.position = _target;
                Velocity = 0;
                onVelocityChange.Invoke();
            }

            if (!(_stunTimeout > 0))
            {
                return;
            }

            if (!(Time.time > _stunTimeout))
            {
                return;
            }

            _stunTimeout = 0;
            onVelocityChange.Invoke();
        }

        void FixedUpdate()
        {
            if (Velocity > 0)
            {
                gameObject.transform.position =
                    Vector3.MoveTowards(gameObject.transform.position, _target, Time.deltaTime * speed);
            }
        }
    }
}