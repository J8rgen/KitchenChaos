using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour {


    [SerializeField] private StoveCounter stoveCounter; // Reference to the StoveCounter script component
    
    private AudioSource audioSource; // Reference to the AudioSource component attached to this GameObject

    private float warningSoundTimer;
    private bool playWarningSound;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged; // Subscribe to the OnStateChanged of stoveCounter
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnShowProgressAmount = .5f;
        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {

        // Determine if the sound should be played based on the current state of the stoveCounter
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        
        if (playSound) {
            // If playSound is true, play the audio
            audioSource.Play();
        }
        else {
            // If playSound is false, pause the audio
            audioSource.Pause();
        }

    }

    private void Update() {
        if (playWarningSound) {
            
            warningSoundTimer -= Time.deltaTime;

            if (warningSoundTimer < +0) {
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }


}
