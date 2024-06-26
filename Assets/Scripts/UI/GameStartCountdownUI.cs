using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour {

    private const string NUMBER_POPUP = "NumberPopup";

    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int previousCountdownNumber;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {

        // Subscribe to the game state change event
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide(); // Initially hide UI visual
    }
    private void Update() {
        int countDownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountdownToStartTimer());
        //constantly update the timer
        countdownText.text = countDownNumber.ToString(); // "F2" , "#.##"

        if (previousCountdownNumber != countDownNumber) {
            previousCountdownNumber = countDownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }

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
