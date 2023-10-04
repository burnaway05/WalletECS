using Unity.Collections;
using Unity.Entities;

namespace Wallet
{
    [UpdateAfter(typeof(IncreaseSystem))]
    public partial class ResetSystem : SystemBase
    {
        private EntityQuery _requiredQuery;

        protected override void OnCreate()
        {
            _requiredQuery = GetEntityQuery(ComponentType.ReadOnly(typeof(ResetComponent)));
            RequireAnyForUpdate(_requiredQuery);
        }

        protected override void OnUpdate()
        {
            var entities = _requiredQuery.ToEntityArray(Allocator.Temp);
            for (int i = 0; i < entities.Length; i++)
            {
                var resource = EntityManager.GetComponentData<ResourceComponent>(entities[i]);
                resource.Amount = 0;
                EntityManager.SetComponentData(entities[i], resource);
                EntityManager.RemoveComponent<ResetComponent>(entities[i]);
            }

            EntityManager.CreateEntity(ComponentType.ReadOnly(typeof(RepaintRequiredComponent)));
        }
    }
}