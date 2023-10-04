using Unity.Collections;
using Unity.Entities;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;

namespace Wallet
{
    public partial class LoadSystem : SystemBase
    {
        private EntityQuery _requiredQuery;

        protected override void OnCreate()
        {
            _requiredQuery = GetEntityQuery(ComponentType.ReadOnly(typeof(FinishedLoadingComponent)));
            RequireForUpdate(_requiredQuery);
        }

        protected override void OnUpdate()
        {
            EntityCommandBuffer buffer = new EntityCommandBuffer(Allocator.TempJob);
            Entities.ForEach((Entity entity, ref ResourceComponent resource, ref FinishedLoadingComponent load) =>
            {
                ResourceComponent loadedResource = JsonUtility.FromJson<ResourceComponent>(load.SaveInfo.Value);
                buffer.SetComponent(entity, loadedResource);
                buffer.RemoveComponent<FinishedLoadingComponent>(entity);
            }).Run();

            buffer.Playback(EntityManager);
            buffer.Dispose();
        }
    }
}