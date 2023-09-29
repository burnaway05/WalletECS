using UnityEngine;

public interface IWalletWindow
{
    public void Initialize(IWalletPresenter presenter);
}

public interface IWalletPresenter
{

}

public class MainWindow : MonoBehaviour, IWalletWindow
{
    private IWalletPresenter _presener;
    [SerializeField] private ResourceSlot _coinSlot;
    [SerializeField] private ResourceSlot _gemSlot;

    public void Initialize(IWalletPresenter presenter)
    {
        _presener = presenter;
    }
}