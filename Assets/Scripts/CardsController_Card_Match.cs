using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsController_Card_Match : MonoBehaviour
{


    public int pairsToMatch;

    [SerializeField] Card_Match cardPrefab;
    [SerializeField] public Transform cardParent;
    [SerializeField] private Sprite[] sprites;
    private List<Sprite> spritePairs;
    Card_Match firstSelected;
    Card_Match SecondSelected;


    void Start()
    {

        PrepareSprites();
        CreateCards();
    }

    public void CreateCards() //create cards and pairs from card prefabs
    {
        for (int i = 0; i < spritePairs.Count; i++)
        {
            Card_Match newCard = Instantiate(cardPrefab, cardParent);
            newCard.SelectIconSprite(spritePairs[i]);
            newCard.gameObject.name = "Card_" + i;
            newCard.ShowIcon();
        }
    }

    public void PrepareSprites()  //based on difficulty level load no of cards pair-from enum
    {
        spritePairs = new List<Sprite>();
       

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

            if (firstSelected == null)
            {
                firstSelected = card;
                return;
            }
            if (SecondSelected == null)
            {
                SecondSelected = card;
                firstSelected = null;
                SecondSelected = null;
            }
        }
    }

  


    
   
}

