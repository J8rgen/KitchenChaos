using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    public static GameInput Instance {  get; private set; }

    public event EventHandler OnInteractAction; 
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    // Reference to player input actions from the new Input System
    private PlayerInputActions playerInputActions;


    private void Awake() {

        Instance = this;

        // Initialize the playerInputActions object
        playerInputActions = new PlayerInputActions();
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

}
