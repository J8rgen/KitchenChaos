using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    //for saving volume level
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance {  get; private set; }

    // hold references to audio clips
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;


    private float volume = 1f; 


    // Subscribe to events when the SoundManager starts
    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void Awake() {
        Instance = this;

        //used if there is no save data
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f); 
    }



    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e) {
        // Cast sender to TrashCounter to get the specific instance
        TrashCounter trashCounter = sender as TrashCounter;
        // Play a sound from audioClipRefsSO using the trash audio clip at the trashCounter's position
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e) {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    public void PlayFootstepSound(Vector3 position, float volume) {
        // Play footstep sounds from audioClipRefsSO at the specified position
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }


    //####


    //Calls the other PlaySound function if there are multipe audio clips
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        //// Randomly select an audio clip from the array
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
        // // Play a single audio clip at a specified position
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }


    public void ChangeVolume() {
        volume += .1f;
        if (volume > 1f) {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save(); // Unity also does this automatically, failsafe
    }


    public float GetVolume() {
        return volume;
    }
}