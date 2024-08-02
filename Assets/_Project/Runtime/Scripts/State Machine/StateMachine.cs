using System;
using System.Collections.Generic;

namespace CoffeeDrop
{
    public class StateMachine{
        StateNode Current;
        Dictionary<Type, StateNode> Nodes = new();
        HashSet<ITransition> AnyTransitions =   new();
        public void Update(){
            var transition =   GetTransition();
            if(transition != null){
                ChangeState(transition.To);
            }
            Current.State?.Update();
        }
        public void FixedUpdate(){
            Current.State?.FixedUpdate();
        }
        public void SetState(IState state){
            Current = Nodes[state.GetType()];
            Current.State?.OnEnter();
        }
        private void ChangeState(IState state){
            if(state == Current.State) return;
            var previousState   =   Current.State;
            var nextState= Nodes[state.GetType()].State;
            previousState?.OnExit();
            nextState?.OnEnter();
            Current = Nodes[state.GetType()];
        }
        private ITransition GetTransition()
        {
            foreach(var transition in AnyTransitions){
                if(transition.Condition.Evaluate()){
                    return transition;
                }
            }
            foreach(var transition in Current.Transitions){
                if(transition.Condition.Evaluate()){
                    return transition;
                }
            }
            return null;
        }
        public void AddTransition(IState from, IState to, IPredicate condition){
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }
        public void AddAnyTransition(IState to, IPredicate condition){
            AnyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }
        private StateNode GetOrAddNode(IState state){
            var node = Nodes.GetValueOrDefault(state.GetType());
            if(node == null){
                node = new StateNode(state);
                Nodes.Add(state.GetType(), node);
            }
            return node;
        }
        class StateNode{
            public IState State { get;}
            public HashSet<ITransition> Transitions { get;}
            public StateNode(IState state){
                State = state;
                Transitions =  new HashSet<ITransition>();
            }
            public void AddTransition(IState to , IPredicate condition ){
                Transitions.Add(new Transition(to, condition));
            }
        }
    }
}