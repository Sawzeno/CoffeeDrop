using UnityEngine;
using Game.SpawnSystem;
using Game.Events.Channel;
namespace Game.Collectibles
{
    public class IceCube : Entity{
        [SerializeField] int Score;
        [SerializeField] IntEventChannel ScoreChannel;

        void OnTriggerEnter(Collider other){
            if(other.CompareTag("Player")){
                Debug.Log("Player collected IceCube");
                ScoreChannel.Invoke(Score);
            }
        }
    }
}
