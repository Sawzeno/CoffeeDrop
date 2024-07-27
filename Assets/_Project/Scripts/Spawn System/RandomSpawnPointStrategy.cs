using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoffeeDrop
{
    public class RandomSpawnPointStrategy : ISpawnPointStrategy
    {
        List<Transform> UnusedSpawnPoints;
        Transform[] SpawnPoints;
        public RandomSpawnPointStrategy(Transform[] spawnPoints)
        {
            SpawnPoints = spawnPoints;
            UnusedSpawnPoints = new List<Transform>(spawnPoints);
        }
        public Transform NextSpawnPoint()
        {
            if(!UnusedSpawnPoints.Any()){
                UnusedSpawnPoints = new List<Transform>(SpawnPoints);
            }
            var randomIndex =   Random.Range(0, UnusedSpawnPoints.Count);
            Transform result = UnusedSpawnPoints[randomIndex];
            UnusedSpawnPoints.RemoveAt(randomIndex);
            return result;
        }
    }
}
