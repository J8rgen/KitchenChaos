using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI recipesDeliveredText;



    private void Start() {

        // Subscribe to the game state change event
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide(); // Initially hide UI visual
    }


    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (KitchenGameManager.Instance.IsGameOver()) {
            Show(); // Show UI

            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
        }
        else {
            Hide(); // Hide UI
        }
    }

    private void Show() {
        gameObject.SetActive(true); // Activate the UI GameObject
    }


    private void Hide() {
        gameObject.SetActive(false); // Deactivate the UI GameObject
    }

}
