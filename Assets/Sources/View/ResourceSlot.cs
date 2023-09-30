using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResourceSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private Button _increase;
    [SerializeField] private Button _reset;

    public void Initialize(UnityAction onIncreaseClick, UnityAction onResetClick)
    {
        _increase.onClick.RemoveAllListeners();
        _reset.onClick.RemoveAllListeners();

        _increase.onClick.AddListener(onIncreaseClick);
        _reset.onClick.AddListener(onResetClick);
    }

    public void SetAmount(int amount)
    {
        _amount.text = amount.ToString();
    }
}