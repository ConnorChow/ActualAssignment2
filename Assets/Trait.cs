using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trait : MonoBehaviour {
    public int characterInt;
    public FlipCardsManager flipCardsManager;
    // Start is called before the first frame update
    void Start() {
        flipCardsManager = GameObject.Find("Card-Board").GetComponent<FlipCardsManager>();
    }

    // Update is called once per frame
    void Update() {

    }
}
