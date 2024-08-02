using UnityEngine;
using Timers;

namespace CoffeeDrop
{
    public class CollectibleSpawnManager : EntitySpawnManager
    {
        [SerializeField] CollectibleData[] CollectibleData;
        [SerializeField] float SpawnInterval = 1f;
        EntitySpawner<Collectible> Spawner;
        CountdownTimer SpawnTimer;
        int Counter;
        protected override void Awake()
        {
            base.Awake();
            Spawner = new EntitySpawner<Collectible>(new EntityFactory<Collectible>(CollectibleData), /*base*/SpawnPointStrategy);
            SpawnTimer = new CountdownTimer(SpawnInterval);
            SpawnTimer.OnTimerStop += () => {
                if(Counter >= SpawnPoints.Length){
                    SpawnTimer.Stop();
                    return;
                }
                Spawn();
                Counter++;
                SpawnTimer.Start();

            };
        }
        void Start() => SpawnTimer.Start();
        public override void Spawn() => Spawner.Spawn();

    }
}
