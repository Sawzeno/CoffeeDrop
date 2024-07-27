using UnityEngine;

namespace CoffeeDrop
{
    public interface IState{
        void OnEnter();
        void Update();
        void FixedUpdate();
        void OnExit();
    }
}