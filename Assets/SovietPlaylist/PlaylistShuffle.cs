using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaylistShuffle : MonoBehaviour
{
    [SerializeField] private AudioClip[] songs;
    private int[] playlistOrder;
    HashSet<int> availableSlots= new HashSet<int>();

    int currentSong = 0;

    private AudioSource source;

    public float desiredVolume = 0.25f;

    // Start is called before the first frame update
    void Start() {
        source = GetComponent<AudioSource>();

        System.Random random = new System.Random();

        playlistOrder= new int[songs.Length];
        for (int i = 0; i < songs.Length; i++) {
            availableSlots.Add(i);
        }

        int newIndex;

        for (int i = 0; i < songs.Length; i++) {
            newIndex = availableSlots.ElementAt(random.Next(availableSlots.Count));
            playlistOrder[i] = newIndex;
        }

        source.clip = songs[playlistOrder[currentSong]];
        source.Play();
    }

    // Update is called once per frame
    void Update(){
        if (!source.isPlaying) {
            currentSong++;
            source.clip = songs[playlistOrder[currentSong]];
            source.Play();
        }

        if (source.volume < desiredVolume) {
            source.volume += Time.deltaTime/2;
        } else if (source.volume > desiredVolume) {
            source.volume -= Time.deltaTime/2;
        }
    }
}
