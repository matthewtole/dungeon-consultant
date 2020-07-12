using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonManager {
    public class CharacterAnimation : MonoBehaviour {
        [SerializeField]
        protected Animator animator;

        [SerializeField]
        protected string horizontalFloat = "Horizontal";
        [SerializeField]
        protected string verticalFloat = "Vertical";
        [SerializeField]
        protected string velocityFloat = "Velocity";
        [SerializeField]
        protected string idleBoolean = "Idle";
        [SerializeField]
        protected string attackTrigger = "Attack";
        [SerializeField]
        protected string hitTrigger = "Hit";
        [SerializeField]
        protected string actionTrigger = "Action";

        protected float idleTimeout;
        protected float idleTimer;

        void Start() {
            Stop();
            IdleOff();
            FaceDirection(Vector3.down);
            EnableAutoIdle();
        }
        void Update() {
            if (idleTimer > 0 && Time.time >= idleTimer) {
                Idle();
            }
        }

        public void FaceDirection(Vector3 position) {
            animator.SetFloat(horizontalFloat, (position - transform.position).x);
            animator.SetFloat(verticalFloat, (position - transform.position).y);
        }

        public void GetHit(Vector3 from) {
            Stop();
            IdleOff();
            FaceDirection(from);
            animator.SetTrigger(hitTrigger);
            ResetIdleTimer();
        }

        public void Attack(Vector3 target) {
            Stop();
            IdleOff();
            FaceDirection(target);
            animator.SetTrigger(attackTrigger);
            ResetIdleTimer();
        }

        public void Walk(Vector3 direction) {
            IdleOff();
            FaceDirection(direction);
            animator.SetFloat(velocityFloat, 1f);
            idleTimer = 0;
        }

        public void Stop() {
            IdleOff();
            animator.SetFloat(velocityFloat, 0f);
            ResetIdleTimer();
        }

        public void Idle() {
            Stop();
            animator.SetBool(idleBoolean, true);
            ResetIdleTimer();
        }

        public void IdleOff() {
            animator.SetBool(idleBoolean, false);
            ResetIdleTimer();
        }

        public void EnableAutoIdle(float timeout = 5f) {
            idleTimeout = timeout;
            ResetIdleTimer();
        }

        public void DisableAutoIdle() {
            idleTimeout = 0;
            ResetIdleTimer();
        }

        protected void ResetIdleTimer() {
            if (idleTimeout > 0) {
                idleTimer = Time.time + idleTimeout;
            }
            else {
                idleTimeout = 0;
            }
        }
    }

}