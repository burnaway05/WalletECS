using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Wallet
{
    public partial class LoadFromPlayerPrefsSystem : SystemBase
    {
        private EntityQuery _requiredQuery;

        protected override void OnCreate()
        {
            _requiredQuery = GetEntityQuery(ComponentType.ReadOnly(typeof(LoadFromPlayerPrefsComponent)));
            RequireForUpdate(_requiredQuery);
        }

        protected override void OnUpdate()
        {
            EntityCommandBuffer buffer = new EntityCommandBuffer(Allocator.TempJob);
            Entities.ForEach((Entity entity, ref ResourceComponent resource, ref LoadFromPlayerPrefsComponent save) =>
            {
                var key = save.Key+ "_" + resource.Type;
                if (PlayerPrefs.HasKey(key))
                {
                    resource.Amount = PlayerPrefs.GetInt(key, resource.Amount);
                    buffer.SetComponent(entity, resource);
                }

                buffer.RemoveComponent<LoadFromPlayerPrefsComponent>(entity);
            }).Run();

            buffer.Playback(EntityManager);
            buffer.Dispose();
        }
    }
}