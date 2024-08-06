namespace Game.Actors.Components
{
    public interface IVisitable{
        public void Accept(IVisitor visitor);
    }
}
