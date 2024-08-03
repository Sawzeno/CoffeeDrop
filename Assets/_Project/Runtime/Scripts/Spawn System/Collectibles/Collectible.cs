using UnityEngine;

namespace CoffeeDrop
{
    public class Collectible : Entity{
        [SerializeField] int Score;
        [SerializeField] IntEventChannel ScoreChannel;

        void OnTriggerEnter(Collider other){
            if(other.CompareTag("Player")){
                Debug.Log("Player collected Collectible");
                ScoreChannel.Invoke(Score);
            }
        }
    }
}
