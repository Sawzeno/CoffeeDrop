using UnityEngine;

namespace CoffeeDrop
{
    public interface ISpawnPointStrategy {
        Transform NextSpawnPoint();
    }
}
