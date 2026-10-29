using UnityEngine;
using TMPro;

public class ScriptUIManager : MonoBehaviour
{
    public static ScriptUIManager Instance { get; private set; }

    [Header("UI Text References")]
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI coinText;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverCoinText;
    public TextMeshProUGUI gameOverScoreText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (ScriptGameManager.Instance != null)
        {
            UpdateHP(ScriptGameManager.Instance.CurrentHP);
            UpdateCoins(ScriptGameManager.Instance.TotalCoins, 0);
        }
    }

    public void UpdateHP(int hp)
    {
        if (hpText != null)
            hpText.text = $"HP: {hp}";
    }

    public void UpdateCoins(int totalCoins, int progress)
    {
        if (coinText != null)
            coinText.text = $"Food: {totalCoins} ({progress}/100)";
    }

    public void ShowGameOver(int totalCoins)
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverCoinText != null)
            gameOverCoinText.text = $"Food Collected: {totalCoins}";

        if (gameOverScoreText != null)
            gameOverScoreText.text = $"Score: {totalCoins * 10}";
    }
}