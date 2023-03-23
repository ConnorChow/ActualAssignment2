using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trait : MonoBehaviour {
    //index to keep track of which character is hidden under this card
    public int characterInt;
    //index to keep track of it's element in the Manager's array
    public int index;
    //reference to the scene's manager
    public FlipCardsManager flipCardsManager;
    // Start is called before the first frame update
    void Start() {
        //get the FlipCardsManager in the scene by name
        flipCardsManager = GameObject.Find("Card-Board").GetComponent<FlipCardsManager>();
    }

    //Hooked up to each button for when is is pressed
    public void OnButtonPress() {
        //if card isn't already flipped, then reveal it to the player
        if (flipCardsManager.flipped[index] == false && GetComponent<Image>().sprite == flipCardsManager.redStar) {
            //try adding this to flipped cards 
            if (flipCardsManager.flipped1 == -1) {
                flipCardsManager.flipped1 = index;
                SetCharacter();
                GetComponent<AudioSource>().Play();
            } else if (flipCardsManager.flipped2 == -1) {
                flipCardsManager.flipped2 = index;
                SetCharacter();
                GetComponent<AudioSource>().Play();
            }
        }
    }
    //this changes the image of the card to the hidden character when it is flipped
    private void SetCharacter() {
        Image img = GetComponent<Image>();
        if (img != null) {
            //switch statement decides which sprite the card is assigned based on characterInt
            switch (characterInt) {
                case 0:
                    img.sprite = flipCardsManager.lenin;
                    break;
                case 1:
                    img.sprite = flipCardsManager.luxemburg;
                    break;
                case 2:
                    img.sprite = flipCardsManager.mao;
                    break;
                case 3:
                    img.sprite = flipCardsManager.min;
                    break;
                case 4:
                    img.sprite = flipCardsManager.sankara;
                    break;
                case 5:
                    img.sprite = flipCardsManager.stalin;
                    break;
                case 6:
                    img.sprite = flipCardsManager.castro;
                    break;
                case 7:
                    img.sprite = flipCardsManager.che;
                    break;
            }
        }
    }
    //for hiding the card when the game is over, or a match is not found
    public void HideCard() {
        Image img = GetComponent<Image>();
        img.sprite = flipCardsManager.redStar;
    }
}
