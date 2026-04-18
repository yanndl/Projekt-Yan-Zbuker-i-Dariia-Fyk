using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText; // Distance UI
    public TextMeshProUGUI coinText;  // Coins UI

    public float score; // Distance value
    private int coins;  // Coins value

    void Awake() {
        instance = this;
    }

    void Update() {
        // Distance logic
        score += Time.deltaTime * 10f;
        if (scoreText != null) {
            scoreText.text = "Distance: " + ((int)score).ToString();
        }
    }

    // Method to add coins
    public void AddCoin() {
        coins++;
        if (coinText != null) {
            coinText.text = "Coins: " + coins.ToString();
        }
    }

    public void ResetScore() {
        score = 0;
        coins = 0;
    }
}
