using Unity.Entities;

namespace Wallet
{
    public partial class InitializeSystem : SystemBase
    {
        protected override void OnCreate()
        {
            Entity coinEntity = EntityManager.CreateEntity();
            EntityManager.AddComponentData(coinEntity, new ResourceComponent(ResourceType.Coin, 5));

            Entity gemEntity = EntityManager.CreateEntity();
            EntityManager.AddComponentData(gemEntity, new ResourceComponent(ResourceType.Gem, 10));
            EntityManager.CreateEntity(ComponentType.ReadOnly(typeof(RepaintRequiredComponent)));
        }

        protected override void OnUpdate()
        {
        }
    }
}