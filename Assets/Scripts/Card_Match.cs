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
    public CardsController_Card_Match cardsController; //reference to the CardsController script
    [SerializeField] private float flipDuration = 0.3f;
    private bool isFlipping = false;

  
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

        Debug.Log("Showing Icon");
        isSelected = true;
        StartCoroutine(FlipCard(IconSprite, true));
    }

    public void HideIcon()
    {

        StartCoroutine(HideIconDelayed());
        StartCoroutine(FlipCard(hiddenIconSprite, false));

    }

    private IEnumerator HideIconDelayed()
    {
        while (isFlipping)
            yield return null;

        StartCoroutine(FlipCard(hiddenIconSprite, false));
        isFlipping = false;
        yield return null;
    }

    private IEnumerator FlipCard(Sprite targetSprite, bool selected)
    {
        isFlipping = true;
        float elapsed = 0f;

        while (elapsed < flipDuration / 2)
        {
            elapsed += Time.deltaTime;
            float scale = Mathf.Lerp(1f, 0f, elapsed / (flipDuration / 2));
            transform.localScale = new Vector3(scale, 1f, 1f);
            yield return null;
        }

        iconImage.sprite = targetSprite;
        isSelected = selected;

        elapsed = 0f;
        while (elapsed < flipDuration / 2)
        {
            elapsed += Time.deltaTime;
            float scale = Mathf.Lerp(0f, 1f, elapsed / (flipDuration / 2));
            transform.localScale = new Vector3(scale, 1f, 1f);
            yield return null;
        }

        transform.localScale = Vector3.one;
        isFlipping = false;
    }
    public void SetInteractable(bool interactable)
    {
        GetComponent<Button>().interactable = interactable;
    }
   
}
