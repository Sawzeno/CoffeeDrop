using UnityEngine;
using App.Timers;
using Game.SpawnSystem;


namespace Game.Collectibles
{
    public class CoffeeBeanSpawnManager : EntitySpawnManager
    {
        [SerializeField] CoffeeBeanData[] CollectibleData;
        [SerializeField] float SpawnInterval = 1f;
        EntitySpawner<CoffeBean> Spawner;
        CountdownTimer SpawnTimer;
        int Counter;
        protected override void Awake()
        {
            base.Awake();
            Spawner = new EntitySpawner<CoffeBean>(new EntityFactory<CoffeBean>(CollectibleData), /*base*/SpawnPointStrategy);
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
