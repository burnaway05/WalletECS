using Mono.Cecil;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    void SaveToPlayerPrefs(string key);
    void LoadFromPlayerPrefs(string key);
    void SaveToFile(string fileName);
    void LoadFromFile(string fileName);
}

public class MainWindow : MonoBehaviour, IWalletWindow
{
    [SerializeField] private ResourceSlot _coinSlot;
    [SerializeField] private ResourceSlot _gemSlot;
    [SerializeField] private TMP_InputField _keyInput;
    [SerializeField] private TMP_InputField _fileNameInput;
    [SerializeField] private Button _saveToPlayerPrefs;
    [SerializeField] private Button _loadFromPlayerPrefs;
    [SerializeField] private Button _saveToFile;
    [SerializeField] private Button _loadFromFile;

    private IWalletPresenter _presener;

    public void Initialize(IWalletPresenter presenter)
    {
        _presener = presenter;
        _coinSlot.Initialize(() => _presener.AddCoin(), () => _presener.ResetCoins());
        _gemSlot.Initialize(() => _presener.AddGem(), () => _presener.ResetGems());
        _saveToPlayerPrefs.onClick.AddListener(() => _presener.SaveToPlayerPrefs(_keyInput.text));
        _loadFromPlayerPrefs.onClick.AddListener(() => _presener.LoadFromPlayerPrefs(_keyInput.text));
        _saveToFile.onClick.AddListener(() => _presener.SaveToFile(_fileNameInput.text));
        _loadFromFile.onClick.AddListener(() => _presener.LoadFromFile(_fileNameInput.text));
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