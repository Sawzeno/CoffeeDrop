using UnityEngine;

namespace CoffeeDrop
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] float MaxHealth = 100;
        [SerializeField] FloatEventChannel PlayerHealthChannel;

        float CurrentHealth;

        void Awake()
        {
            CurrentHealth = MaxHealth;
        }
        void Start()
        {
            PublishHealthPercentage();
        }
        public void TakeDamage(int damage)
        {
            if (CurrentHealth > 0)
            {
                CurrentHealth -= (float)damage;
                PublishHealthPercentage();
            }
        }

        private void PublishHealthPercentage()
        {
            if (PlayerHealthChannel != null)
            {
                PlayerHealthChannel.Invoke(CurrentHealth / MaxHealth);
            }
        }

        public bool IsDead => CurrentHealth <= 0;

    }
}
