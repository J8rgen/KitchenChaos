using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour {


    [SerializeField] private StoveCounter stoveCounter; // Reference to the StoveCounter script component
    private AudioSource audioSource; // Reference to the AudioSource component attached to this GameObject

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged; // Subscribe to the OnStateChanged of stoveCounter
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {

        // Determine if the sound should be played based on the current state of the stoveCounter
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        
        if ( playSound) {
            // If playSound is true, play the audio
            audioSource.Play();
        }
        else {
            // If playSound is false, pause the audio
            audioSource.Pause();
        }
    }
}
