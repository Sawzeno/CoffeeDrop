namespace Game
{
    public interface IVisitor{
        void Visit(ActorHealth healthComponent);
        void Visit(ActorMagic magicComponent);
    }
}
