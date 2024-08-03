using UnityEngine;
using System;
using System.Collections.Generic;

namespace CoffeeDrop
{
    public abstract class EventChannelSO<T> : ScriptableObject
    {
        readonly HashSet<EventListener<T>> Observers = new();

        public void Invoke(T value){
            foreach(var observer in Observers){
                observer.Raise(value);
            }
        }
        public void Register(EventListener<T> observer) => Observers.Add(observer);
        public void Deregrister(EventListener<T> observer) => Observers.Remove(observer);
    }

    public readonly struct Empty{};
    [CreateAssetMenu(menuName = "Events/EventChannel")]
    public class EventChannel :EventChannelSO<Empty>{}
}
