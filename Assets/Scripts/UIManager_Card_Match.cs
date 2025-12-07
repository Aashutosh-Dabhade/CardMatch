using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager_Card_Match : MonoBehaviour
{
    public static UIManager_Card_Match Instance;

    [Header("Score UI")]
    public TMP_Text scoreText;//players current score
    public TMP_Text highScoreText; //high score of all time
    [Header("Timer UI")]
    public TMP_Text timerText; //to show time remaining
    public TMP_Text showAllText;// show preview time remaining to memorize
    public Slider timerSlider;// slider to show time remaining

     [Header("Panels")]
    public GameObject winPanel; //player win planel
    public GameObject gameOverPanel;// game over panle
    public TMP_Text winScoreText;// show score on player win panel
    public TMP_Text winHighScoreText;//show high score on player win panel
    public TMP_Text gameOverScoreText;//show players current score onn player win panle
    public TMP_Text gameOverHighScoreText;//show phigh score on game ovver panel


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
 public void ShowWinPanel(bool show) //show this panel if player wins
    {
        if (winPanel != null)
            winPanel.SetActive(show);

        if (show)
        {
            if (winScoreText != null)
                winScoreText.text = $"Score: {PlayerData_Card_Match.Instance.score}";
            if (winHighScoreText != null)
                winHighScoreText.text = $"High Score: {PlayerData_Card_Match.Instance.highScore}";
        }
    }

    public void ShowGameOverPanel(bool show) // show game over panel 
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(show);

        if (show)
        {
            if (gameOverScoreText != null)
                gameOverScoreText.text = $"Score: {PlayerData_Card_Match.Instance.score}";
            if (gameOverHighScoreText != null)
                gameOverHighScoreText.text = $"High Score: {PlayerData_Card_Match.Instance.highScore}";
        }
    }
  public void UpdateScoreUI(int score, int highScore)// update current and high score
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
        if (highScoreText != null)
            highScoreText.text = $"High Score: {highScore}";
    }
    public void UpdateTimerUI(float timeLeft)//update gameplay time left
    {
        if (timerText != null)
            timerText.text = Mathf.Ceil(timeLeft).ToString();

        if (timerSlider != null)
            timerSlider.value = timeLeft;
    }

    public void UpdateShowAllUI(float showAllTime)// update preview timer
    {
        if (showAllText != null)
            showAllText.text = "Memorize: " + Mathf.Ceil(showAllTime).ToString();
    }

    public void SetShowAllTextActive(bool active) //show memorize text and time remaing for it.
    {
        if (showAllText != null)
            showAllText.gameObject.SetActive(active);
    }
}