using Unity.Entities;

namespace Wallet
{
    public class WalletPresenter : IWalletPresenter
    {
        private EntityManager _entityManager;

        public WalletPresenter()
        {
            var world = new World("WalletWorld");
            _entityManager = world.EntityManager;
        }
    }
}