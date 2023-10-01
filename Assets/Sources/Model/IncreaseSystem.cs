using Unity.Collections;
using Unity.Entities;

namespace Wallet
{
    [UpdateAfter(typeof(LoadSystem))]
    public partial class IncreaseSystem : SystemBase
    {
        private EntityQuery _requiredQuery;

        protected override void OnCreate()
        {
            _requiredQuery = GetEntityQuery(ComponentType.ReadOnly(typeof(IncreaseComponent)));
        }

        protected override void OnUpdate()
        {
            var entities = _requiredQuery.ToEntityArray(Allocator.Temp);
            for (int i = 0; i < entities.Length; i++)
            {
                var resource = EntityManager.GetComponentData<ResourceComponent>(entities[i]);
                resource.Amount++;
                EntityManager.SetComponentData(entities[i], resource);
                EntityManager.RemoveComponent<IncreaseComponent>(entities[i]);
            }
        }
    }
}