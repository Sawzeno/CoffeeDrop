using UnityEngine;

namespace CoffeeDrop
{
    public class LinearSpawnPointStrategy : ISpawnPointStrategy
    {
        int index  = 0;
        Transform[] SpawnPoints;
        public LinearSpawnPointStrategy(Transform[] spawnPoints){
            SpawnPoints = spawnPoints;
        }

        public Transform NextSpawnPoint()
        {
            Transform result = SpawnPoints[index];
            index = (index + 1) % SpawnPoints.Length;
            return result;
        }
    }
}
