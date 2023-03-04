using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trait : MonoBehaviour {
    public int characterInt;
    public int index;

    public FlipCardsManager flipCardsManager;
    // Start is called before the first frame update
    void Start() {
        flipCardsManager = GameObject.Find("Card-Board").GetComponent<FlipCardsManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnButtonPress() {
        if (flipCardsManager.flipped[index] == false && GetComponent<Image>().sprite == flipCardsManager.redStar) {
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
    private void SetCharacter() {
        Image img = GetComponent<Image>();
        if (img != null) {
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
    public void HideCard() {
        Image img = GetComponent<Image>();
        img.sprite = flipCardsManager.redStar;
    }
}
