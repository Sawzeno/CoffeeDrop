using UnityEngine;

namespace CoffeeDrop
{
    public class EnemyHealth : MonoBehaviour{
        [SerializeField] int MaxHealth = 50;
        [SerializeField] FloatEventChannel PlayerHealthChannel;

        int CurrentHealth;

        void Awake(){
            CurrentHealth = MaxHealth;
        }
        void Start(){
            PublishHealthPercentage();
        }
        public void TakeDamage(int damage){
            MaxHealth -= damage;
            PublishHealthPercentage();
        }

        private void PublishHealthPercentage()
        {
            if(PlayerHealthChannel != null){
                PlayerHealthChannel.Invoke(CurrentHealth / (float)MaxHealth);
            }
        }

        public bool IsDead  =>  CurrentHealth <= 0;

    }
}
