using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace CoffeeDrop
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {get; private set;}
        public int Score{get; private set;}

        void Awake(){
            if(Instance == null){
                Instance = this;
            }else{
                Debug.Log("Game Manager was already created, detroying that to create a new one");
                Destroy(gameObject);
            }
        }

        public void AddScore(int score){
            Score += score;
        }
    }
}
