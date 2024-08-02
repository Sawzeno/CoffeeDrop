using System;

namespace CoffeeDrop
{
    public class FuncPredicate : IPredicate
    {
        readonly Func<bool> Func;

        public FuncPredicate(Func<bool> func){
            Func    =   func;
        }
        public bool Evaluate() => Func.Invoke();
    }
}