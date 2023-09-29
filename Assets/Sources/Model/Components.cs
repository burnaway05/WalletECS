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
}