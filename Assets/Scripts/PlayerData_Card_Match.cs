using UnityEngine;

public class PlayerData_Card_Match: MonoBehaviour
{
    public static PlayerData_Card_Match Instance;

    public int score = 0;
    public int highScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value)
    {
        score += value;
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }
    }

    public void ResetScore() // reset score to zero
    {
        score = 0;
    }

    public void SaveHighScore() //to save high score
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public void LoadHighScore() //load high score from playerprefs
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}