using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class musicPlayer : MonoBehaviour {

    AudioSource songPlayer;
    static bool isPlaying = false;

    private void Awake()
    {
        songPlayer = GetComponent<AudioSource>();
        if(!isPlaying)
        {
            songPlayer.Play();
            isPlaying = true;
        }

        DontDestroyOnLoad(this);
    }
}
