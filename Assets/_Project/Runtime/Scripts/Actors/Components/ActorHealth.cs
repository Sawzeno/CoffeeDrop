using UnityEngine;
using Game.Events.Channel;
namespace Game.Actors.Components
{
    public class ActorHealth : MonoBehaviour, IVisitable
    {
        float CurrentHealth;
        [SerializeField] float MaxHealth = 100;
        [SerializeField] FloatEventChannel HealthChannel;
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
        }
        public void AddHealth(float health)
        {
            if ((CurrentHealth + health) >= MaxHealth)
            {
                Debug.Log("max boost");
                CurrentHealth = MaxHealth;
            }
            else
            {
                Debug.Log("one boost");
                CurrentHealth += health;
            }
            PublishHealthPercentage();
        }
        public bool IsDead => CurrentHealth <= 0;
    }
}
