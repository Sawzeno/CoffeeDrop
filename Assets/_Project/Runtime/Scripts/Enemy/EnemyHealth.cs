using UnityEngine;

namespace CoffeeDrop
{
    public class EnemyHealth : MonoBehaviour{
        [SerializeField] float MaxHealth = 50;
        [SerializeField] FloatEventChannel PlayerHealthChannel;

        float CurrentHealth;

        void Awake(){
            CurrentHealth = MaxHealth;
        }
        void Start(){
            PublishHealthPercentage();
        }
        public void TakeDamage(int damage){
            CurrentHealth -= (float)damage;
            Debug.Log($"current health : {CurrentHealth}");
            PublishHealthPercentage();
        }

        private void PublishHealthPercentage()
        {
            if(PlayerHealthChannel != null){
                PlayerHealthChannel.Invoke(CurrentHealth / MaxHealth);
            }
        }

        public bool IsDead  =>  CurrentHealth <= 0;

    }
}
