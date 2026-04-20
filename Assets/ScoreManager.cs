using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager instance;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;     // Distance
    public TextMeshProUGUI coinText;      // Coins
    public TextMeshProUGUI highscoreText; // Best score

    [Header("Current Stats")]
    public float score;
    private int coins;
    private float highscore;

    void Awake() {
        instance = this;

        // Load highscore from memory
        highscore = PlayerPrefs.GetFloat("Highscore", 0);
        UpdateHighscoreUI();
    }

    void Update() {
        // Distance tracking logic
        score += Time.deltaTime * 10f;
        if (scoreText != null) {
            scoreText.text = "Distance: " + ((int)score).ToString();
        }

        // Check and save new highscore
        if (score > highscore) {
            highscore = score;
            PlayerPrefs.SetFloat("Highscore", highscore);
            UpdateHighscoreUI();
        }
    }

    private void UpdateHighscoreUI() {
        if (highscoreText != null) {
            highscoreText.text = "Best: " + ((int)highscore).ToString();
        }
    }

    public void AddCoin() {
        coins++;
        if (coinText != null) {
            coinText.text = "Coins: " + coins.ToString();
        }
    }

    // This method fixes the error in PlayerController.cs
    public void ResetScore() {
        score = 0;
        coins = 0;

        // Refresh UI after reset
        if (coinText != null) coinText.text = "Coins: 0";
    }
}