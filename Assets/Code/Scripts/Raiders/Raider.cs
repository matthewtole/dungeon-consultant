using Code.Scripts.Characters;
using UnityEngine;

namespace Code.Scripts.Raiders
{
    public interface IDamageable
    {
        void TakeDamage(Transform from, float amount);
    }

    public class Raider : MonoBehaviour, IDamageable
    {
        [SerializeField] protected ObjectList raiderList;
        [SerializeField] protected CharacterAnimation characterAnimation;
        [SerializeField] protected CharacterMovement characterMovement;

        public string characterName;
        public float maxHealth = 100;

        public float currentHealth;
        public float currentFear = 0;

        private void Awake()
        {
            raiderList.Add(gameObject);
            currentHealth = maxHealth;
        }

        private void OnDestroy()
        {
            raiderList.Remove(gameObject);
        }

        public void TakeDamage(Transform from, float amount)
        {
            characterAnimation.GetHit(from.position);

            transform.position = Vector3Int.RoundToInt(transform.position);
            characterMovement.Stun(1f);

            currentHealth -= amount;
            currentFear += 10;
        }

        private void Update()
        {
            if (currentFear > 0)
            {
                currentFear -= 0.001f;
            }
        }
    }
}