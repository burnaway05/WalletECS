using UnityEngine;

namespace Wallet
{
    public class Run : MonoBehaviour
    {
        private WalletPresenter _presenter;

        void Start()
        {
            _presenter = new WalletPresenter();

            IWalletWindow view = FindObjectOfType<MainWindow>();
            view.Initialize(_presenter); 
        }
    }
}