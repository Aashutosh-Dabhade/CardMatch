using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsController_Card_Match : MonoBehaviour
{
    public enum Difficulty { Easy, Medium, Hard }

    [SerializeField] private Difficulty gameDifficulty = Difficulty.Easy;
    [SerializeField] private int easyPairs = 4;
    [SerializeField] private int mediumPairs = 8;
    [SerializeField] private int hardPairs = 12;
    private int pairsToMatch;

    [SerializeField] Card_Match cardPrefab;
    [SerializeField] public Transform cardParent;
    [SerializeField] private Sprite[] sprites;
    private List<Sprite> spritePairs;
    Card_Match firstSelected;
    Card_Match SecondSelected;
    int matchCount;

    void Start()
    {
        PlayerData_Card_Match.Instance.ResetScore(); // reset score to zero
        PlayerData_Card_Match.Instance.LoadHighScore(); // load high score from playerprefs
        UIManager_Card_Match.Instance.UpdateScoreUI(PlayerData_Card_Match.Instance.score, PlayerData_Card_Match.Instance.highScore); 
        PrepareSprites();
        CreateCards();
    }

    public void CreateCards() //create cards and pairs from card prefabs
    {
        for (int i = 0; i < spritePairs.Count; i++)
        {
            Card_Match newCard = Instantiate(cardPrefab, cardParent);
            newCard.SelectIconSprite(spritePairs[i]);
            newCard.cardsController = this;
            newCard.gameObject.name = "Card_" + i;
            newCard.ShowIcon();
        }
    }

    public void PrepareSprites()  //based on difficulty level load no of cards pair-from enum
    {
        spritePairs = new List<Sprite>();
        switch (gameDifficulty)
        {
            case Difficulty.Easy:
                pairsToMatch = easyPairs;
                break;
            case Difficulty.Medium:
                pairsToMatch = mediumPairs;
                break;
            case Difficulty.Hard:
                pairsToMatch = hardPairs;
                break;
        }

        for (int i = 0; i < pairsToMatch; i++)
        {
            spritePairs.Add(sprites[i]);
            spritePairs.Add(sprites[i]);
        }

        for (int i = 0; i < spritePairs.Count; i++)
        {
            int randomIndex = Random.Range(i, spritePairs.Count);
            Sprite temp = spritePairs[i];
            spritePairs[i] = spritePairs[randomIndex];
            spritePairs[randomIndex] = temp;
        }
    }

    public void SetSelected(Card_Match card) //select and show clicked icon and check if its matching
    {
        if (card.isSelected)
        {
            card.ShowIcon();
            AudioManager_Match_Card.Instance.PlayShowIcon();
            if (firstSelected == null)
            {
                firstSelected = card;
                return;
            }
            if (SecondSelected == null)
            {
                SecondSelected = card;
                StartCoroutine(CheckMAtching(firstSelected, SecondSelected));
                firstSelected = null;
                SecondSelected = null;
            }
        }
    }

    IEnumerator CheckMAtching(Card_Match a, Card_Match b) //check if first and second selected card is matching
    {
        yield return new WaitForSeconds(0.3f);
        if (a.IconSprite == b.IconSprite)
        {
            matchCount++;
            PlayerData_Card_Match.Instance.AddScore(10);
            UIManager_Card_Match.Instance.UpdateScoreUI(PlayerData_Card_Match.Instance.score, PlayerData_Card_Match.Instance.highScore);

            AudioManager_Match_Card.Instance.PlayCardMatch();
            a.SetInteractable(false);
            b.SetInteractable(false);

            if (matchCount >= spritePairs.Count / 2)
            {
                Timer_Card_Match.instance.StopTimer();
                AddTimeBonus();
                AudioManager_Match_Card.Instance.PlayWin();
                UIManager_Card_Match.Instance.ShowWinPanel(true);
            }
        }
        else
        {
            a.HideIcon();
            b.HideIcon();
        }
    }

    public void GameOverEvent()
    {
        AudioManager_Match_Card.Instance.PlayGameOver();
        UIManager_Card_Match.Instance.ShowGameOverPanel(true);
        Timer_Card_Match.instance.StopTimer();
    }

    public void RestartGame() // restart game at current level.set slider and timer to zero
    {
        foreach (Transform child in cardParent)
        {
            Destroy(child.gameObject);
        }
        Timer_Card_Match.instance.StopTimer();
        matchCount = 0;
        firstSelected = null;
        SecondSelected = null;
        UIManager_Card_Match.Instance.ShowWinPanel(false);
        UIManager_Card_Match.Instance.ShowGameOverPanel(false);

        PlayerData_Card_Match.Instance.ResetScore();
        UIManager_Card_Match.Instance.UpdateScoreUI(PlayerData_Card_Match.Instance.score, PlayerData_Card_Match.Instance.highScore);

        PrepareSprites();
        CreateCards();
        Timer_Card_Match.instance.SetTimer();
    }

    public void SetDifficulty(Difficulty difficulty) 
    {
        Timer_Card_Match.instance.SetTimer();
        gameDifficulty = difficulty;
    }
    public void SetDifficulty(int difficulty) // for setting difficulty from button
    {
        Timer_Card_Match.instance.SetTimer();
        gameDifficulty = (Difficulty)difficulty;
        RestartGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private void AddTimeBonus()
    {
        float remainingTime = Mathf.Max(0, Timer_Card_Match.instance.playDuration - (Timer_Card_Match.instance.playDuration - float.Parse(Timer_Card_Match.instance.timerText.text)));
        int timeBonus = Mathf.RoundToInt(remainingTime * 2);
        PlayerData_Card_Match.Instance.AddScore(timeBonus);
        UIManager_Card_Match.Instance.UpdateScoreUI(PlayerData_Card_Match.Instance.score, PlayerData_Card_Match.Instance.highScore);
    }
}

