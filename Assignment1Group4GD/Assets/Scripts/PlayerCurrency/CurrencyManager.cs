using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI currencyText;    // Text displaying currency
    [SerializeField] private Image currencyFillImage;         // Image with fillAmount (0 to 1)
    [SerializeField] private Slider currencySlider;           // Optional: Slider UI

    [Header("Currency Settings")]
    [SerializeField] private int maxCurrency = 2000;          // Max currency used for fill gauge

    public int CurrentCurrency { get; private set; } = 0;

     [SerializeField] private UpgradePauseController uiManager;

      [SerializeField] private int maxIncreasePerCap = 10;

    private void Start()
    {
        UpdateCurrencyUI();
    }

    public void AddCurrency(int amount)
    {
        CurrentCurrency += amount;
        CurrentCurrency = Mathf.Clamp(CurrentCurrency, 0, maxCurrency);
        UpdateCurrencyUI();
        if (CurrentCurrency == maxCurrency)
        {
            uiManager.OpenUpgradeMenu(); 
            maxCurrency += maxIncreasePerCap;
        }

    }

    public void SpendCurrency(int amount)
    {
        if (amount <= CurrentCurrency)
        {
            CurrentCurrency -= amount;
            UpdateCurrencyUI();
        }
    }

    private void UpdateCurrencyUI()
    {
        // Update text
        if (currencyText != null)
        {
            currencyText.text = $"{CurrentCurrency}";
        }

        // Update fill image (0 to 1)
        if (currencyFillImage != null)
        {
            currencyFillImage.fillAmount = (float)CurrentCurrency / maxCurrency;
        }

        // Update optional slider
        if (currencySlider != null)
        {
            currencySlider.maxValue = maxCurrency;
            currencySlider.value = CurrentCurrency;
        }
    }

    public void SetMaxCurrency(int newMax)
    {
        maxCurrency = newMax;
        CurrentCurrency = Mathf.Clamp(CurrentCurrency, 0, maxCurrency);
        UpdateCurrencyUI();
    }
}



