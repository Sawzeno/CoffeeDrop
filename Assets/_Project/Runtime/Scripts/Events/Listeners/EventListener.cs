using UnityEngine;
using UnityEngine.Events;

namespace Game.Events.Channel
{
    public abstract class EventListener<T> : MonoBehaviour{
        // listen to a certain channel and fire a unity event when that happens
        [SerializeField] EventChannelSO<T> EventChannel;
        [SerializeField] UnityEvent<T> UnityEvent;
        protected void Awake(){
            EventChannel.Register(this);
        }
        public void Raise(T value){
            UnityEvent?.Invoke(value);
        }
        protected void OnDestroy(){
            EventChannel.Deregrister(this);
        }
    };
    public class EventListener : EventListener<Empty>{}
    
}
