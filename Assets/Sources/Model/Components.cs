using Unity.Collections;
using Unity.Entities;

namespace Wallet
{
    public enum ResourceType
    {
        Coin,
        Gem,
    }

    public struct ResourceComponent : IComponentData
    {
        public ResourceType Type;
        public int Amount;

        public ResourceComponent(ResourceType type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }

    public struct IncreaseComponent : IComponentData
    {
    }

    public struct ResetComponent : IComponentData
    {
    }

    public struct SaveToPlayerPrefsComponent: IComponentData
    {
        public FixedString128Bytes Key;

        public SaveToPlayerPrefsComponent(FixedString128Bytes key)
        {
            Key = key;
        }
    }

    public struct LoadFromPlayerPrefsComponent: IComponentData
    {
        public FixedString128Bytes Key;

        public LoadFromPlayerPrefsComponent(FixedString128Bytes key)
        {
            Key = key;
        }
    }
}