using UnityEngine;
using Game.SpawnSystem;
using Game.Actors.Components;

namespace Game.Collectibles
{
    public class IceCube : Entity{
        public PowerUp PowerUp;
        void OnTriggerEnter(Collider other){
            var visitable = other.GetComponent<IVisitable>();
            if(visitable != null){
                visitable.Accept(PowerUp);
                // handle destruciton is spawner
                Destroy(gameObject);
            }
        }
    }
}
