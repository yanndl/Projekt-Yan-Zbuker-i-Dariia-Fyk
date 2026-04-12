using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    public float score;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        score += Time.deltaTime * 10f; // скорость роста счёта
        scoreText.text = ((int)score).ToString();
    }

    public void ResetScore()
    {
        score = 0;
    }
}
