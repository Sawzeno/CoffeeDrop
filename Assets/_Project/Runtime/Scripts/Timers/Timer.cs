using System;
using UnityEngine;

namespace Timers
{
    public class CountdownTimer : Timer
    {
        public CountdownTimer(float value) : base(value) {}
        public override void Tick()
        {
            if(IsRunning && CurrentTime  > 0){
                CurrentTime -= Time.deltaTime;
            }
            if(IsRunning && CurrentTime <= 0){
                Stop();
            }
        }
        public override bool IsFinished => CurrentTime <= 0;
    }
    public abstract class Timer : IDisposable{
        public float CurrentTime { get; protected set;}
        public bool IsRunning{get;private set;}
        protected float InitialTime;
        public float Progress => Mathf.Clamp(CurrentTime/InitialTime, 0, 1);
        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate  { };
        protected Timer(float value){
            InitialTime = value;
        }
        public void Start(){
            CurrentTime = InitialTime;
            if(!IsRunning){
                IsRunning = true;
                TimerManager.RegisterTimer(this);
                OnTimerStart.Invoke();
            }
        }
        public void Stop(){
            if(IsRunning){
                IsRunning = false;
                TimerManager.DeregisterTimer(this);
                OnTimerStop.Invoke();
            }
        }
        public abstract void Tick();
        public abstract bool IsFinished { get;}
        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;
        public virtual void Reset() => CurrentTime = InitialTime;
        public virtual void Reset(float newTime){
            InitialTime     =   newTime;
            Reset();
        }
        // ------------------------Destruction Logic--------------------
// you want to be able to call it from the finalizer and let the implimentor override if they want
// Call Dispose to ensure deregistration of the timer from the TimerManager
// when the consumer is done with the timer or being Destroyed 
        bool Disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing){
            if(Disposed) return;
            if(disposing){
                TimerManager.DeregisterTimer(this);
            }
            Disposed = true;
        }
        ~Timer(){
            Dispose(false);
        }
    }
}
