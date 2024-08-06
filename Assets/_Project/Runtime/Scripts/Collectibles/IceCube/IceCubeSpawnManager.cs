using UnityEngine;
using App.Timers;
using Game.SpawnSystem;

namespace Game.Collectibles
{
    public class IceCubeSpawnManager : EntitySpawnManager
    {
        [SerializeField] IceCubeData[] IceCubeData;
        [SerializeField] float SpawnInterval = 1f;
        EntitySpawner<IceCube> Spawner;
        CountdownTimer SpawnTimer;
        int Counter;
        protected override void Awake()
        {
            base.Awake();
            Spawner = new EntitySpawner<IceCube>(new EntityFactory<IceCube>(IceCubeData), /*base*/SpawnPointStrategy);
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
