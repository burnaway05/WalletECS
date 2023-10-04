using UnityEngine;

namespace Wallet
{
    public class Run : MonoBehaviour
    {
        private WalletPresenter _presenter;

        private void Start()
        {
            IWalletWindow view = FindObjectOfType<MainWindow>();

            _presenter = new WalletPresenter(view);
            view.Initialize(_presenter); 
        }

        private void Update()
        {
            _presenter.Update();
        }
    }
}