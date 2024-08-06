using UnityEngine;
using Utils;

namespace Game
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public int Score{get; private set;}

        public void AddScore(int score){
            Score += score;
        }
    }
}
