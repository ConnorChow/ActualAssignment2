using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FlipCardsManager : MonoBehaviour {
    //Textures for flipping cards

    //Assign the pic of Mao here
    public Sprite mao;
    //Assign the pic of Stalin here
    public Sprite stalin;
    //Assign the pic of Lenin here
    public Sprite lenin;
    //Assign the pic of Sankara here
    public Sprite sankara;
    //Assign the pic of Luxemburg here
    public Sprite luxemburg;
    //Assign the pic of Min here
    public Sprite min;
    //Assign the pic of Che here
    public Sprite che;
    //Assign the pic of Castro here
    public Sprite castro;
    //Assign the pic of the red star here
    public Sprite redStar;

    //flag that rises into the scene when the player wins
    public GameObject flagObj;

    //array of all the cards in the scene (Buttons with images on them)
    GameObject[] cards = new GameObject[16];
    //array of all trait script components of the cards in the scene (they must have this component already)
    Trait[] traits = new Trait[16];
    //boolean array to track which cards have been successfully flipped
    public bool[] flipped = new bool[16];

    //boolean to indicate when the game is complete
    bool complete = false;

    //index of the pair of cards flipped by player, -1 represents nothing selected, 0+ represents a card index in the game
    
    //First card flipped
    public int flipped1 = -1;
    //Second Card flipped
    public int flipped2 = -1;
    //pull from this array of available ints representing characters (2 of each since you need to match them)
    int[] availableLib = new int[16] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
    //Used to grab a random character type (based on int) and set each individual card
    HashSet<int> available = new HashSet<int>();

    //Generate random to shuffle the card layout
    private static readonly System.Random r = new System.Random();

    //For background playlist
    PlaylistShuffle music;

    // Start is called before the first frame update
    void Start() {
        //fill the hashset with the potential character int representations
        for (int i = 0; i < cards.Length; i++)
            available.Add(i);
        //We use this variable to generate random indexes of the hashset
        int randomIndex;
        //for loop characterizes all the cards by pulling random ints from the hashet and populating the buttons trait type with it
        for (int i = 0; i < cards.Length; i++) {
            //fetches and assigns the cards based on name
            cards[i] = GameObject.Find("Card (" + i + ")");
            randomIndex = available.ElementAt(r.Next(available.Count));

            //sets the characters hidden beinf each card
            traits[i] = cards[i].GetComponent<Trait>();
            traits[i].characterInt = availableLib[randomIndex];
            traits[i].index = i;

            flipped[i] = false;

            available.Remove(randomIndex);
        }
        //delay for when two incorrect cards are flipped
        timer = delay;

        music = GameObject.Find("CommieBackgroundMusic").GetComponent<PlaylistShuffle>();
    }

    //Data for delay/timer setup... when a player flips two unmatching cards how long until the card is flipped back over
    public float delay = 2.0f;
    //decrements based on delay length
    float timer;

    // Update is called once per frame
    void Update() {
        //Manages when two cards are flipped
        if (flipped1 != -1 && flipped2 != -1) {
            //If the cards don't match, then hide them again, otherwise leave them face up and set their flipped elements to true
            if (cards[flipped1].GetComponent<Trait>().characterInt == cards[flipped2].GetComponent<Trait>().characterInt) {
                flipped[flipped1] = true;
                flipped[flipped2] = true;
                
                flipped1 = -1;
                flipped2 = -1;
            } else {
                timer -= Time.deltaTime;
                if (timer <= 0) {
                    cards[flipped1].GetComponent<Trait>().HideCard();
                    cards[flipped2].GetComponent<Trait>().HideCard();

                    flipped1 = -1;
                    flipped2 = -1;

                    timer = delay;
                }
            }
        }
        //The for loop with this boolean checks on each frame whether the player has completed the round of puzzles
        bool allFinished = true;
        for (int i = 0; i < flipped.Length; i++) {
            if (!flipped[i])
                allFinished = false;
        }
        //If allFinished makes it through the check, we complete the game, silence the background radio, and play the Soviet Anthem
        if (!complete && allFinished) {
            complete = true;
            GetComponent<AudioSource>().Play();
            music.desiredVolume = 0;
        }
        //behaviour for when the game is complete
        if (complete) {
            //Upon complete, this gradually moves the victory flag up the to center of the screen
            if (flagObj.transform.position.y < 0) {
                flagObj.transform.Translate(0, Time.deltaTime/2, 0);
            }
            //reset the scene when the space button is pressed
            if (Input.GetKeyDown("space")) {
                ResetScene();
            }
        }
        //quit the game on esc being pressed. NOTE! only works in a built version
        if (Input.GetKeyDown("escape")){
            Application.Quit();
        }
    }
    //Resets all variables to their default values, reshuffles the cards and esssentially restarts the scene
    //without abruptly cutting off and restarting the radio. Repeats almost everything is Start()
    private void ResetScene() {
        music.desiredVolume = 0.25f;
        GetComponent<AudioSource>().Stop();
        flagObj.transform.position = new Vector3(0, -12, 0);
        complete = false;

        for (int i = 0; i < cards.Length; i++)
            available.Add(i);

        int randomIndex;

        for (int i = 0; i < cards.Length; i++) {
            cards[i].GetComponent<Trait>().HideCard();

            randomIndex = available.ElementAt(r.Next(available.Count));

            traits[i].characterInt = availableLib[randomIndex];
            traits[i].index = i;

            flipped[i] = false;

            available.Remove(randomIndex);
        }
        timer = delay;

        flipped1 = -1;
        flipped2 = -1;
    }
}
