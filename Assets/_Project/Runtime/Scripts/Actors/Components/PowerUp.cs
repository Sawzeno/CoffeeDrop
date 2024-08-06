using UnityEngine;

namespace Game.Actors.Components
{
    [CreateAssetMenu(fileName="PowerUp", menuName="PowerUps")]
    public class PowerUp : ScriptableObject, IVisitor
    {
        public float HealthBonus = 10;
        public float MagicRecharge=10;
        public void Visit(ActorHealth healthComponent)
        {
            healthComponent.AddHealth(HealthBonus);

        }

        public void Visit(ActorMagic magicComponent)
        {
            Debug.Log("VISIT MAGIC");
        }
    }
}
