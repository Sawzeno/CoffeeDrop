using System.ComponentModel;

namespace Game.Actors.Components
{
    public interface IVisitor{
        void Visit(ActorHealth healthComponent);
        void Visit(ActorMagic magicComponent);
    }
}
