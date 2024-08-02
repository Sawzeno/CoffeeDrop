using UnityEngine;

namespace CoffeeDrop
{
    public abstract class EntitySpawnManager : MonoBehaviour{
        [SerializeField] protected SpawnPointStrategyType SpawnStrategyType = SpawnPointStrategyType.Linear;
        [SerializeField] protected Transform[] SpawnPoints;
        protected ISpawnPointStrategy SpawnPointStrategy;
        protected enum SpawnPointStrategyType{
            Linear,
            Random
        }
        protected virtual void Awake(){
            SpawnPointStrategy = SpawnStrategyType switch{
                SpawnPointStrategyType.Linear => new LinearSpawnPointStrategy(SpawnPoints),
                SpawnPointStrategyType.Random => new RandomSpawnPointStrategy(SpawnPoints),
                _ => SpawnPointStrategy
            };
        }
        public abstract void Spawn();
    }
}
