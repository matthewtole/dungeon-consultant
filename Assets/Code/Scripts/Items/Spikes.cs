using Code.Scripts.Raiders;
using UnityEngine;

namespace Code.Scripts.Items
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] protected Animator animator;

        public bool autoActivate = true;

        private bool _isActive = false;
        private bool _damageDisabled = false;
        private static readonly int Active = Animator.StringToHash("Active");

        void Start()
        {
            UpdateLayer();
            if (autoActivate)
            {
                InvokeRepeating(nameof(Activate), 5, 5);
            }
        }

        private void UpdateLayer()
        {
            // TODO: Figure out a better way to do this!
            //gameObject.layer = LayerMask.NameToLayer(_isActive ? "Object" : "Floor");
        }

        private void Activate()
        {
            _isActive = !_isActive;
            animator.SetBool(Active, _isActive);
            _damageDisabled = false;
            UpdateLayer();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!_isActive || _damageDisabled)
            {
                return;
            }

            IDamagable dm = collision.GetComponent<IDamagable>();
            if (dm == null)
            {
                return;
            }

            dm.TakeDamage(transform, 10);
            _damageDisabled = true;

        }
    }
}