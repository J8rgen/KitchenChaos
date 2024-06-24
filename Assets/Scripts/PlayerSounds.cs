using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    private Player player;                // Player component / gameobject
    private float footstepTimer;          // Timer for footstep sounds
    private float footstepTimerMax = .1f; //Maximum time between footstep sounds




    private void Awake() {
        player = GetComponent<Player>(); // Get the Player component attached to the same GameObject
    }

    private void Update() {
        footstepTimer -= Time.deltaTime; //// Decrease footstep timer based on real time

        // Check if it's time to play a footstep sound
        if (footstepTimer < 0f) {
            footstepTimer = footstepTimerMax; // Reset footstep timer

            // Check if the player is currently walking
            if (player.IsWalking()) {

                float volume = 2f; // Volume of the footstep sound

                // Play the footstep sound at the player's current position
                SoundManager.Instance.PlayFootstepSound(player.transform.position, volume);
            }

        }
    }

}
