using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour {

    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged; // state changes for UI visuals


    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state; // Current game state
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer = 10f;

    private void Awake() {

        Instance = this; // Singleton instance

        state = State.WaitingToStart; // Initial state
    }

    private void Update() {

        // Update based on current game state
        switch (state) {
            case State.WaitingToStart:

                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f) {
                    state = State.CountdownToStart;

                    OnStateChanged?.Invoke(this, EventArgs.Empty); // for UI visuals
                }
                break;

            case State.CountdownToStart:

                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f) {
                    state = State.GamePlaying;

                    OnStateChanged?.Invoke(this, EventArgs.Empty); // for UI visuals
                }
                break;

            case State.GamePlaying:

                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f) {
                    state = State.GameOver;

                    OnStateChanged?.Invoke(this, EventArgs.Empty); // for UI visuals
                }
                break;
                
            case State.GameOver:

                break;
        }

    }

    // Check if the game is currently in playing state
    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    // Check if the countdown to start is currently active
    public bool IsCountdownToStartActive() {
        return state == State.CountdownToStart;
    }

    // Get the remaining countdown time before the game starts
    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }

}
