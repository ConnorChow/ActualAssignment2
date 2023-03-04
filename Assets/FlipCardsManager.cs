using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FlipCardsManager : MonoBehaviour {
    //Textures for flipping cards
    public Sprite mao;
    public Sprite stalin;
    public Sprite lenin;
    public Sprite sankara;
    public Sprite luxemburg;
    public Sprite min;
    public Sprite che;
    public Sprite castro;
    public Sprite redStar;

    GameObject[] cards = new GameObject[16];
    Trait[] traits = new Trait[16];
    public bool[] flipped = new bool[16];

    //boolean to indicate when the game is complete
    bool complete = false;

    //index of the pair of cards flipped by player, -1 is nothing selected
    public int flipped1 = -1;
    public int flipped2 = -1;

    int[] availableLib = new int[16] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
    HashSet<int> available = new HashSet<int>();
    int[] characterLib = new int[16];

    private static readonly System.Random r = new System.Random();

    // Start is called before the first frame update
    void Start() {

        for (int i = 0; i < cards.Length; i++)
            available.Add(i);

        int randomIndex;

        for (int i = 0; i < cards.Length; i++) {
            cards[i] = GameObject.Find("Card (" + i + ")");
            randomIndex = available.ElementAt(r.Next(available.Count));

            traits[i] = cards[i].GetComponent<Trait>();
            traits[i].characterInt = availableLib[randomIndex];
            traits[i].index = i;

            flipped[i] = false;

            available.Remove(randomIndex);
        }
        timer = delay;
    }

    public float delay = 2.0f;
    float timer;

    // Update is called once per frame
    void Update() {
        if (flipped1 != -1 && flipped2 != -1) {
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
        bool allFinished = true;
        for (int i = 0; i < flipped.Length; i++) {
            if (!flipped[i])
                allFinished = false;
        }
        if (!complete && allFinished) {
            complete = true;
            GetComponent<AudioSource>().Play();
        }
        if (complete && Input.GetKeyDown("space")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
