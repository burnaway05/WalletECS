using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Wallet
{
    public partial class SaveToPlayerPrefsSystem : SystemBase
    {
        private EntityQuery _requiredQuery;

        protected override void OnCreate()
        {
            _requiredQuery = GetEntityQuery(ComponentType.ReadOnly(typeof(SaveToPlayerPrefsComponent)));
            RequireForUpdate(_requiredQuery);
        }

        protected override void OnUpdate()
        {
            EntityCommandBuffer buffer = new EntityCommandBuffer(Allocator.TempJob);
            Entities.ForEach((Entity entity, ref ResourceComponent resource, ref SaveToPlayerPrefsComponent save) =>
            {
                var key = save.Key + "_" + resource.Type;
                PlayerPrefs.SetInt(key, resource.Amount);

                buffer.RemoveComponent<SaveToPlayerPrefsComponent>(entity);
            }).Run();

            buffer.Playback(EntityManager);
            buffer.Dispose();
        }
    }
}