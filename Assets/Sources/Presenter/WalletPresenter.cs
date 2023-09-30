using Unity.Collections;
using Unity.Entities;

namespace Wallet
{
    public class WalletPresenter : IWalletPresenter
    {
        private World _world;

        public WalletPresenter()
        {
            _world = World.DefaultGameObjectInjectionWorld;
            //_world = new World("WalletWorld");
            //World.DefaultGameObjectInjectionWorld = _world;
            //_world.CreateSystem<LoadSystem>();
            //_world.CreateSystemManaged<IncreaseSystem>();
            //_world.CreateSystem<ResetSystem>();
        }

        public int GetCoinAmount()
        {
            return GetResourceAmount(ResourceType.Coin);
        }

        public int GetGemAmount()
        {
            return GetResourceAmount(ResourceType.Gem);
        }

        private int GetResourceAmount(ResourceType type)
        {
            var entities = GetResources();
            for (int i = 0; i < entities.Length; i++)
            {
                var resource = _world.EntityManager.GetComponentData<ResourceComponent>(entities[i]);
                if (resource.Type == type)
                {
                    return resource.Amount;
                }
            }

            return 0;
        }

        private NativeArray<Entity> GetResources()
        {
            EntityQuery resourceQuery = _world.EntityManager.CreateEntityQuery(ComponentType.ReadOnly<ResourceComponent>());
            if (!resourceQuery.IsEmptyIgnoreFilter)
            {
                var entities = resourceQuery.ToEntityArray(Allocator.Temp);
                return entities;
            }

            return new NativeArray<Entity>();
        }

        public void AddCoin()
        {
            var entities = GetResources();
            for (int i = 0; i < entities.Length; i++)
            {
                var resource = _world.EntityManager.GetComponentData<ResourceComponent>(entities[i]);
                if (resource.Type == ResourceType.Coin)
                {
                    _world.EntityManager.AddComponent<IncreaseComponent>(entities[i]);
                }
            }
        }

        public void AddGem()
        {
            var entities = GetResources();
            for (int i = 0; i < entities.Length; i++)
            {
                var resource = _world.EntityManager.GetComponentData<ResourceComponent>(entities[i]);
                if (resource.Type == ResourceType.Gem)
                {
                    _world.EntityManager.AddComponentData(entities[i], new IncreaseComponent());
                }
            }
        }

        public void ResetCoins()
        {
            var entities = GetResources();
            for (int i = 0; i < entities.Length; i++)
            {
                var resource = _world.EntityManager.GetComponentData<ResourceComponent>(entities[i]);
                if (resource.Type == ResourceType.Coin)
                {
                    _world.EntityManager.AddComponentData(entities[i], new ResetComponent());
                }
            }
        }

        public void ResetGems()
        {
            var entities = GetResources();
            for (int i = 0; i < entities.Length; i++)
            {
                var resource = _world.EntityManager.GetComponentData<ResourceComponent>(entities[i]);
                if (resource.Type == ResourceType.Gem)
                {
                    _world.EntityManager.AddComponentData(entities[i], new ResetComponent());
                }
            }
        }
    }
}