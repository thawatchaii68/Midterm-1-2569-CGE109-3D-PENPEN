using UnityEngine;

public class ScriptGameManager : MonoBehaviour
{
    public static ScriptGameManager Instance { get; private set; }

    public enum GameState { MainMenu, Playing, Paused, GameOver }
    public GameState State { get; private set; } = GameState.Playing;

    public int startingHP = 3;
    public int maxHP = 9;
    public int CurrentHP { get; private set; }

    public int coinsPerExtraHP = 100;
    public int TotalCoins { get; private set; }
    private int coinProgress;

    [Header("Speed Settings")]
    public float baseSpeed = 8f;        
    public float speedMultiplier = 0.2f; 
    public float maxSpeed = 20f;         

    // คำนวณความเร็วปัจจุบันตาม TotalCoins
    public float CurrentSpeed => Mathf.Min(baseSpeed + (TotalCoins * speedMultiplier), maxSpeed);

    public bool IsPlaying => State == GameState.Playing;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start() => StartGame();

    public void StartGame()
    {
        CurrentHP = startingHP;
        TotalCoins = 0;
        coinProgress = 0;
        State = GameState.Playing;
        Time.timeScale = 1f;

        ScriptUIManager.Instance?.UpdateHP(CurrentHP);
        ScriptUIManager.Instance?.UpdateCoins(TotalCoins, coinProgress);
    }

    public void TakeDamage(int amount)
    {
        if (!IsPlaying) return;
        CurrentHP -= amount;

        ScriptUIManager.Instance?.UpdateHP(CurrentHP);

        if (CurrentHP <= 0) 
        { 
            CurrentHP = 0; 
            GameOver(); 
        }
    }

    public void AddCoin()
    {
        TotalCoins++;
        coinProgress++;

        if (coinProgress >= coinsPerExtraHP)
        {
            coinProgress -= coinsPerExtraHP;
            AddHP(1); 
        }

        ScriptUIManager.Instance?.UpdateCoins(TotalCoins, coinProgress);
    }

    public void AddHP(int amount)
    {
        CurrentHP = Mathf.Min(CurrentHP + amount, maxHP);
        ScriptUIManager.Instance?.UpdateHP(CurrentHP);
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        State = GameState.GameOver;
        ScriptUIManager.Instance?.ShowGameOver(TotalCoins);
    }

    public void SetPaused(bool paused)
    {
        State = paused ? GameState.Paused : GameState.Playing;
    }
}