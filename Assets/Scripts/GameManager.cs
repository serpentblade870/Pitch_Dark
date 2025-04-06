using UnityEngine;
using TMPro; // Add this for TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI scoreText; // Changed from Text to TextMeshProUGUI
    public TextMeshProUGUI coinText; // Changed from Text to TextMeshProUGUI
    private int score = 0;
    private int coin = 0;
    
    void Awake()
    {
        Instance = this;
    }
    
    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
     public void AddCoins(int amount)
    {
        coin += amount;
        if (coinText != null)
        {
            coinText.text = "Coins: " + coin;
        }
    }
}