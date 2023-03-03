using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class FlipCardsManager : MonoBehaviour {

    GameObject[] cards = new GameObject[16];
    Button[] buttons = new Button[16];

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
            buttons[i] = cards[i].GetComponent<Button>();
            randomIndex = available.ElementAt(r.Next(available.Count));
            characterLib[i] = availableLib[randomIndex];
            available.Remove(randomIndex);
        }
    }

    // Update is called once per frame
    void Update() {
        foreach (var button in buttons) {
        }
    }
}
