using UnityEngine;

namespace Game.SpawnSystem
{
    public interface ISpawnPointStrategy {
        Transform NextSpawnPoint();
    }
}
