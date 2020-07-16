using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] protected float speed = 1f;
        [SerializeField] protected UnityEvent onVelocityChange;
        [SerializeField] protected UnityEvent onDirectionChange;

        private float _velocity = 0f;
        private Vector2 _direction = Vector2.zero;
        private Vector3 _target;

        private float _stunTimeout = 0f;

        public float Velocity => _velocity;

        public Vector2 Direction => _direction;

        public void MoveInDirection(Vector3 moveDirection)
        {
            if (_stunTimeout > 0)
            {
                return;
            }

            if (_velocity > 0)
            {
                return;
            }

            _target = gameObject.transform.position + moveDirection.normalized;
            _velocity = speed;
            _direction = moveDirection;
            onVelocityChange.Invoke();
            onDirectionChange.Invoke();
        }

        public void Stun(float duration)
        {
            _stunTimeout = Time.time + duration;
            _velocity = 0;
            _direction = Vector3.zero;
            _target = gameObject.transform.position;
            onVelocityChange.Invoke();
        }

        private void Update()
        {
            if (_velocity > 0 && (gameObject.transform.position - _target).magnitude < 0.01f)
            {
                gameObject.transform.position = _target;
                _velocity = 0;
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
            if (_velocity > 0)
            {
                gameObject.transform.position =
                    Vector3.MoveTowards(gameObject.transform.position, _target, Time.deltaTime * speed);
            }
        }
    }
}