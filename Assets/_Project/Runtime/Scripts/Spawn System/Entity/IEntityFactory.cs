using UnityEngine;

namespace CoffeeDrop
{
    public interface IEntityFactory<T> where T : Entity {
        T create(Transform spawnPoint);
    }
}
