using UnityEngine;
using Game.Events.Channel;
namespace Game
{
    public class ActorMagic : MonoBehaviour, IVisitable
    {
        [SerializeField] float MaxMagic = 100;
        [SerializeField] FloatEventChannel MagicChannel;

        public void PublishMagicPercentage()
        {
            throw new System.NotImplementedException();
        }
        public void useMagic()
        {
            throw new System.NotImplementedException();
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            Debug.Log("Magic Component - accept");
        }
    }
}