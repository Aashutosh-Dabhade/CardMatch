using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Match : MonoBehaviour
{
     [SerializeField] private Image iconImage; //icon to show
    public Sprite hiddenIconSprite; //sprite to card icon. in this case shield icon
    public Sprite IconSprite;  //random sprite to be assigned here.
    public bool isSelected = false;
     CardsController_Card_Match cardsController; //reference to the CardsController script
    void Start()
    {
        cardsController = FindObjectOfType<CardsController_Card_Match>();
    }
    public void OnCardClicked() 
    {
        if (isSelected)
        {
            HideIcon();
        }
        else
        {
            ShowIcon();
            cardsController.SetSelected(this);
          
        }
    }

    public void SelectIconSprite(Sprite sp)
    {
        IconSprite = sp;
    }
   
   public void ShowIcon()
    {
       
        iconImage.sprite = IconSprite;
        isSelected = true;
    }
    public void HideIcon()
    {
        iconImage.sprite = hiddenIconSprite;
        isSelected = false;
    }
public void SetInteractable(bool interactable)
{
    GetComponent<Button>().interactable = interactable;
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
