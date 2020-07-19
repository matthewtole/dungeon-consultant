using UnityEngine;

namespace Code.Scripts.Items
{
    public class Torch : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        private static readonly int Lit = Animator.StringToHash("Lit");

        void Start()
        {
            animator.SetBool(Lit, true);
        }

        public void Light()
        {
            animator.SetBool(Lit, true);
        }

        public void PutOut()
        {
            animator.SetBool(Lit, false);
        }
    }
}
