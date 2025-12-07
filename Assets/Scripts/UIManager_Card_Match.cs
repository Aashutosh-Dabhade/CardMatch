using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager_Card_Match : MonoBehaviour
{
    public static UIManager_Card_Match Instance;

    [Header("Timer UI")]
    public TMP_Text timerText; //to show time remaining
    public TMP_Text showAllText;// show preview time remaining to memorize
    public Slider timerSlider;// slider to show time remaining

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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