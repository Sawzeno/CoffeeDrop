using UnityEngine;
using Utils;

namespace CoffeeDrop
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public int Score{get; private set;}

        public void AddScore(int score){
            Score += score;
        }
    }
}
