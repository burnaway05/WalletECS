using Unity.Collections;
using Unity.Entities;

namespace Wallet
{
    [UpdateAfter(typeof(LoadSystem))]
    public class IncreaseSystem : ComponentSystemBase
    {
        private EntityQuery _requiredQuery;

        protected override void OnCreate()
        {
            _requiredQuery = GetEntityQuery(ComponentType.ReadOnly(typeof(IncreaseComponent)));
        }

        public override void Update()
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