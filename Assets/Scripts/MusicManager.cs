using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour {

    //for saving music level
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    public static MusicManager Instance {  get; private set; }

    private AudioSource audioSource;
    private float volume = .3f;

    private void Awake() {

        Instance = this;

        audioSource = GetComponent<AudioSource>();

        //used if there is no save data
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, .3f);
        audioSource.volume = volume; // sound starts playing right away (neccessary)
    }


    public void ChangeVolume() {
        volume += .1f;
        if (volume > 1f) {
            volume = 0f;
        }
        audioSource.volume = volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }


    public float GetVolume() {
        return volume;
    }
}
