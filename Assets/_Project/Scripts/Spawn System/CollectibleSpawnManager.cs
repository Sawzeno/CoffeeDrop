using Unity.VisualScripting;
using UnityEngine;
using Utilities;

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
            SpawnTimer = new(SpawnInterval);
            SpawnTimer.OnTimerStop += () => {
                if(Counter++ >= SpawnPoints.Length){
                    SpawnTimer.Stop();
                }
                Spawn();
                Counter++;
                SpawnTimer.Start();

            };
        }
        void Start() => SpawnTimer.Start();
        void Update() => SpawnTimer.Tick(Time.deltaTime);
        public override void Spawn() => Spawner.Spawn();

    }
}
