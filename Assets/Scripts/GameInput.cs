using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public static GameInput Instance {  get; private set; }

    public event EventHandler OnInteractAction; 
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    // game input bindings
    public enum Binding {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Pause,
    }

    // Reference to player input actions from the new Input System
    private PlayerInputActions playerInputActions;


    private void Awake() {
        Instance = this;

        


        // Initialize the playerInputActions object
        playerInputActions = new PlayerInputActions();

        // if bindings changed ingame load saved bindings
        // must be done after constructing the object and before enabling actionmap
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS)) {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        // Enable the player input actions
        playerInputActions.Player.Enable();

        // Subscribe to events from generated PlayerInputActions
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteraceAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_perforemd;

        
    }

    
    private void OnDestroy() {
        // when this object is destroyed it will unsubscribe from those events
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteraceAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_perforemd;

        playerInputActions.Dispose(); // free the object and clear memory
    }

    private void Pause_perforemd(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty); 
        
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }


    public Vector2 GetMovementVectorNormalized() {

        #region old Input system
        //Vector2 inputVector = new Vector2(0, 0);
        //if (Input.GetKey(KeyCode.W)) {
        //    inputVector.y = +1;
        //}
        //if (Input.GetKey(KeyCode.S)) {
        //    inputVector.y = -1;
        //}
        //if (Input.GetKey(KeyCode.A)) {
        //    inputVector.x = -1;
        //}
        //if (Input.GetKey(KeyCode.D)) {
        //    inputVector.x = +1;
        //}
        #endregion

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized; //same speed in all directions
        //Debug.Log(inputVector); print to terminal
        return inputVector;
    }

    public string GetBindingText(Binding binding) {
        switch (binding) {
            default: 
            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            // ToString returns some other stuff aswell -> ToDisplayString();

            case Binding.InteractAlternate:
                return playerInputActions.Player.InteraceAlternate.bindings[0].ToDisplayString();

            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();

            case Binding.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();

            case Binding.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();

            case Binding.Move_Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();

            case Binding.Move_Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
                //movement ones are in an array starting from [1]   (WASD [0])
                //Debug.Log(GetBindingText(Binding.Interact));
        }
    }


    public void RebindBinding(Binding binding, Action onActionRebound) { 
        // Action built in delegate 
        playerInputActions.Player.Disable(); // disable actionmap

        InputAction inputAction;
        int bindingIndex;
        switch (binding) {
            default:
            case Binding.Move_Up:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = playerInputActions.Player.InteraceAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
        }
        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback => { // callback for when interactive rebinding completes
                //Debug.Log(callback.action.bindings[1].path);
                //Debug.Log(callback.action.bindings[1].overridePath);
                callback.Dispose(); // free memory
                playerInputActions.Player.Enable(); // enable actionmap
                onActionRebound(); // we call HidePressToRebindKey, update visual (passed these functions)


                //Debug.Log(playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                // Save bindings even when the game is exited
                PlayerPrefs.Save(); 
            }) 
            .Start(); // start rebinding process
    }
}
