using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public float score;
    private int highScore;

    void Awake()
    {
        instance = this;

        // Загружаем рекорд из памяти
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        // Показываем рекорд на экране
        highScoreText.text = "BEST: " + highScore.ToString();
    }

    void Update()
    {
        score += Time.deltaTime * 10f;
        scoreText.text = ((int)score).ToString();
    }

    public void CheckHighScore()
    {
        if ((int)score > highScore)
        {
            highScore = (int)score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void ResetScore()
    {
        score = 0;
    }
}
