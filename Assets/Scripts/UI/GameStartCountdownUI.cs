using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start() {

        // Subscribe to the game state change event
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide(); // Initially hide UI visual
    }
    private void Update() {
        //constantly update the timer
        countdownText.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountdownToStartTimer()).ToString(); // "F2" , "#.##"
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e) {
        if ( KitchenGameManager.Instance.IsCountdownToStartActive()) {
            Show(); // Show UI when countdown is active
        }
        else {
            Hide(); // Hide UI when countdown is not active
        }
    }

    private void Show() {
        gameObject.SetActive(true); // Activate the UI GameObject
    }


    private void Hide() {
        gameObject.SetActive(false); // Deactivate the UI GameObject
    }


}
