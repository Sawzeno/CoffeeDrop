using UnityEngine;
using Game.Events.Channel;
namespace Game
{
    public class ActorHealth : MonoBehaviour, IVisitable
    {
        [SerializeField] float MaxHealth = 100;
        [SerializeField] FloatEventChannel HealthChannel;
        float CurrentHealth;
        void Awake()
        {
            CurrentHealth = MaxHealth;
        }
        void Start()
        {
            PublishHealthPercentage();
        }
        public virtual void TakeDamage(int damage)
        {
            if (CurrentHealth > 0)
            {
                CurrentHealth -= (float)damage;
                PublishHealthPercentage();
            }
        }

        public void PublishHealthPercentage()
        {
            if (HealthChannel != null)
            {
                HealthChannel.Invoke(CurrentHealth / MaxHealth);
            }
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            Debug.Log("Health component - accept");
        }

        public bool IsDead => CurrentHealth <= 0;
    }
}
