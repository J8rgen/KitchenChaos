using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour {

    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged; // state changes for UI visuals
    public event EventHandler OnGamePaused;   //listen on GamePauseUI
    public event EventHandler OnGameUnpaused; //listen on GamePauseUI

    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state; // Current game state
    
    private float countdownToStartTimer = 3f;

    private float gamePlayingTimer; 
    private float gamePlayingTimerMax = 30f;
    private bool isGamePaused = false;


    private void Awake() {

        Instance = this; // Singleton instance

        state = State.WaitingToStart; // Initial state
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;

        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction; // if interact we close tutorial

    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) { // if interact we close tutorial
        if (state == State.WaitingToStart) {
            state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        TogglePauseGame();
    }

    private void Update() {

        // Update based on current game state
        switch (state) {
            case State.WaitingToStart:
                //Change this based on player input
                break;

            case State.CountdownToStart:

                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f) {
                    state = State.GamePlaying;

                    gamePlayingTimer = gamePlayingTimerMax; // initialize value, leave constant same for other use

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

    // Check if the game is currently in over state
    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalized() {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax); 
        // 1 - (...): Inverts the fraction, giving a value that starts at 0
    }



    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;

        if (isGamePaused ) {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty );
        }
        else {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }


}
