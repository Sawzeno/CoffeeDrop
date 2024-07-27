namespace CoffeeDrop
{
    public class EntitySpawner<T> where T: Entity{
        IEntityFactory<T> EntityFactory;
        ISpawnPointStrategy SpawnPointStrategy;
        public EntitySpawner(IEntityFactory<T> entityFactory, ISpawnPointStrategy spawnPointStrategy)
        {
            EntityFactory = entityFactory;
            SpawnPointStrategy = spawnPointStrategy;
        }
        public T Spawn(){
            return EntityFactory.create(SpawnPointStrategy.NextSpawnPoint());
        }
    }
}
