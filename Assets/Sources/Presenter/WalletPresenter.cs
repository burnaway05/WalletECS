using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Wallet
{
    public class WalletPresenter : IWalletPresenter
    {
        private World _world;
        private IWalletWindow _window;

        public WalletPresenter(IWalletWindow mainWindow)
        {
            _world = World.DefaultGameObjectInjectionWorld;
            _window = mainWindow;
        }

        public void Update()
        {
            var repaintEntity = _world.EntityManager.CreateEntityQuery(ComponentType.ReadOnly<RepaintRequiredComponent>()).ToEntityArray(Allocator.Temp);
            if (repaintEntity.Length > 0)
            {
                _world.EntityManager.DestroyEntity(repaintEntity);
                _window.Repaint();
            }
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

        public void SaveToPlayerPrefs(string key)
        {
            var entities = GetResources();
            for (int i = 0; i < entities.Length; i++)
            {
                _world.EntityManager.AddComponentData(entities[i], new SaveToPlayerPrefsComponent(key));
            }
        }

        public void LoadFromPlayerPrefs(string key)
        {
            var entities = GetResources();
            for (int i = 0; i < entities.Length; i++)
            {
                _world.EntityManager.AddComponentData(entities[i], new LoadFromPlayerPrefsComponent(key));
            }
        }

        public void SaveToFile(string fileName)
        {
            var entity = _world.EntityManager.CreateEntity();
            _world.EntityManager.AddComponentData(entity, new SaveToFileComponent(fileName));
        }

        public void LoadFromFile(string fileName)
        {
            var entity = _world.EntityManager.CreateEntity();
            _world.EntityManager.AddComponentData(entity, new LoadFromFileComponent(fileName));
        }
    }
}