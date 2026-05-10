using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Need this for Restart
using UnityEngine.UI; // Need this for Button

public class ScoreManager : MonoBehaviour {
    public static ScoreManager instance;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI highscoreText;
    public GameObject restartButton; // Link your button here

    [Header("Current Stats")]
    public float score;
    private int coins;
    private float highscore;
    private bool isDead = false;

    void Awake() {
        instance = this;
        highscore = PlayerPrefs.GetFloat("Highscore", 0);
        UpdateHighscoreUI();
        
        if (restartButton != null) restartButton.SetActive(false);
    }

    void Update() {
        if (isDead) return; // Stop counting distance if dead

        score += Time.deltaTime * 10f;
        if (scoreText != null) {
            scoreText.text = "Distance: " + ((int)score).ToString();
        }

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
        if (isDead) return;
        coins++;
        if (coinText != null) {
            coinText.text = "Coins: " + coins.ToString();
        }
    }

    // New method to show Game Over
    public void GameOver() {
        isDead = true;
        Time.timeScale = 0f; // Freeze the game
        if (restartButton != null) restartButton.SetActive(true); // Show button
    }

    // This method will be called by the Button
    public void RestartGame() {
        Time.timeScale = 1f; // Unfreeze time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetScore() {
        score = 0;
        coins = 0;
        if (coinText != null) coinText.text = "Coins: 0";
    }
}