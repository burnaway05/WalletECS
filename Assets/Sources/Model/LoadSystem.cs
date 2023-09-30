using Unity.Entities;

namespace Wallet
{
    public class LoadSystem : ComponentSystemBase
    {
        protected override void OnCreate()
        {
            Entity coinEntity = EntityManager.CreateEntity();
            EntityManager.AddComponentData(coinEntity, new ResourceComponent(ResourceType.Coin, 0));

            Entity gemEntity = EntityManager.CreateEntity();
            EntityManager.AddComponentData(gemEntity, new ResourceComponent(ResourceType.Gem, 10));
        }

        public override void Update()
        {
        }
    }
}