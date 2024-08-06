using UnityEngine;

namespace Game.SpawnSystem
{
    public interface IEntityFactory<T> where T : Entity {
        T create(Transform spawnPoint);
    }
}
