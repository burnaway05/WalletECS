using Mono.Cecil;
using UnityEngine;

public interface IWalletWindow
{
    void Initialize(IWalletPresenter presenter);
}

public interface IWalletPresenter
{
    int GetCoinAmount();
    int GetGemAmount();
    void AddCoin();
    void AddGem();
    void ResetCoins();
    void ResetGems();
}

public class MainWindow : MonoBehaviour, IWalletWindow
{
    [SerializeField] private ResourceSlot _coinSlot;
    [SerializeField] private ResourceSlot _gemSlot;

    private IWalletPresenter _presener;

    public void Initialize(IWalletPresenter presenter)
    {
        _presener = presenter;
        _coinSlot.Initialize(() => _presener.AddCoin(), () => _presener.ResetCoins());
        _gemSlot.Initialize(() => _presener.AddGem(), () => _presener.ResetGems());
    }

    private void Update()
    {
        Repaint();
    }

    public void Repaint()
    {
        _coinSlot.SetAmount(_presener.GetCoinAmount());
        _gemSlot.SetAmount(_presener.GetGemAmount());
    }
}