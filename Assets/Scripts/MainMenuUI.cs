using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    [SerializeField] private Button playButton; // Button to start the game
    [SerializeField] private Button quitButton; // Button to quit the game

    private void Awake() {

        playButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        }); // lambda (delegates)

        quitButton.onClick.AddListener(() => {
            Application.Quit(); // Quit the application when the quit button is clicked
        }); // lambda (delegates)

    }

}
