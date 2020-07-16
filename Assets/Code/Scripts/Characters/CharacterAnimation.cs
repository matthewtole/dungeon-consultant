using UnityEngine;

namespace Code.Scripts.Characters
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        [SerializeField] protected CharacterMovement movement;

        [SerializeField] protected string horizontalFloat = "Horizontal";
        [SerializeField] protected string verticalFloat = "Vertical";
        [SerializeField] protected string velocityFloat = "Velocity";
        [SerializeField] protected string idleBoolean = "Idle";
        [SerializeField] protected string attackTrigger = "Attack";
        [SerializeField] protected string hitTrigger = "Hit";
        [SerializeField] protected string actionTrigger = "Action";

        private float _idleTimeout;
        private float _idleTimer;

        void Start()
        {
            Stop();
            IdleOff();
            FaceDirection(Vector3.down);
            EnableAutoIdle();
        }

        void Update()
        {
            if (_idleTimer > 0 && Time.time >= _idleTimer)
            {
                Idle();
            }
        }

        public void HandleVelocityChange()
        {
            IdleOff();
            animator.SetFloat(velocityFloat, movement.Velocity);
        }

        public void HandleDirectionChange()
        {
            IdleOff();
            animator.SetFloat(horizontalFloat, movement.Direction.x);
            animator.SetFloat(verticalFloat, movement.Direction.y);
        }

        public void FaceDirection(Vector3 position)
        {
            var currentPosition = transform.position;
            animator.SetFloat(horizontalFloat, (position - currentPosition).x);
            animator.SetFloat(verticalFloat, (position - currentPosition).y);
        }

        public void GetHit(Vector3 from)
        {
            Stop();
            IdleOff();
            FaceDirection(from);
            animator.SetTrigger(hitTrigger);
            ResetIdleTimer();
        }

        public void Attack(Vector3 target)
        {
            Stop();
            IdleOff();
            FaceDirection(target);
            animator.SetTrigger(attackTrigger);
            ResetIdleTimer();
        }

        public void Walk(Vector3 direction)
        {
            IdleOff();
            FaceDirection(direction);
            animator.SetFloat(velocityFloat, 1f);
            _idleTimer = 0;
        }

        public void Stop()
        {
            IdleOff();
            animator.SetFloat(velocityFloat, 0f);
            ResetIdleTimer();
        }

        public void Idle()
        {
            Stop();
            animator.SetBool(idleBoolean, true);
            ResetIdleTimer();
        }

        public void IdleOff()
        {
            animator.SetBool(idleBoolean, false);
            ResetIdleTimer();
        }

        public void EnableAutoIdle(float timeout = 5f)
        {
            _idleTimeout = timeout;
            ResetIdleTimer();
        }

        public void DisableAutoIdle()
        {
            _idleTimeout = 0;
            ResetIdleTimer();
        }

        protected void ResetIdleTimer()
        {
            if (_idleTimeout > 0)
            {
                _idleTimer = Time.time + _idleTimeout;
            }
            else
            {
                _idleTimeout = 0;
            }
        }
    }

}