namespace CoffeeDrop
{
    public class EntitySpawner<T> where T: Entity{
        readonly IEntityFactory<T> EntityFactory;
        readonly ISpawnPointStrategy SpawnPointStrategy;
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
