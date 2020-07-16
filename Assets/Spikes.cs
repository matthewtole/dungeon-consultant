using UnityEngine;

namespace DungeonManager {
    public class Spikes : MonoBehaviour {
        [SerializeField]
        Animator animator;

        public bool autoActivate = true;

        private bool isActive = false;
        private bool damageDisabled = false;

        void Start() {
            if (autoActivate) {
                InvokeRepeating("Activate", 5, 5);
            }
        }

        void Activate() {
            isActive = !isActive;
            animator.SetBool("Active", isActive);
            damageDisabled = false;
        }

        private void OnTriggerStay2D(Collider2D collision) {
            if (!isActive || damageDisabled) {
                return;
            }

            IDamagable dm = collision.GetComponent<IDamagable>();
            if (dm == null) {
                return;
            }
         
            dm.TakeDamage(transform, 10);
            damageDisabled = true;

        }
    }

}