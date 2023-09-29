using Unity.Entities;

namespace Wallet
{
    public class IncreaseSystem : ComponentSystemBase
    {
        private EntityQuery _query;

        protected override void OnCreate()
        {
            _query = GetEntityQuery(ComponentType.ReadOnly(typeof(Coin)));
            RequireForUpdate(_query);
        }

        public override void Update()
        {
        }
    }
}