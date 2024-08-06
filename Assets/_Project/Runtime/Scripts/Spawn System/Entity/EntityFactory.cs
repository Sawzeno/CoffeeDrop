using UnityEngine;

namespace Game.SpawnSystem
{
    public class EntityFactory<T> : IEntityFactory<T> where T : Entity
    {
        EntityDataSO[] Data;
        public EntityFactory(EntityDataSO[] data)
        {
            Data = data;
        }
        public T create(Transform spawnPoint)
        {
            EntityDataSO entityData = Data[Random.Range(0, Data.Length)];
            GameObject instance = Object.Instantiate(entityData.Prefab, spawnPoint.position, spawnPoint.rotation);
            return instance.GetComponent<T>();
        }
    }
}
