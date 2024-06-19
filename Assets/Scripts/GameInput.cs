using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

    public event EventHandler OnInteractAction; 
    public event EventHandler OnInteractAlternateAction;

    // Reference to player input actions from the new Input System
    private PlayerInputActions playerInputActions;


    private void Awake() {

        // Initialize the playerInputActions object
        playerInputActions = new PlayerInputActions();
        // Enable the player input actions
        playerInputActions.Player.Enable();

        // Subscribe to the Interact action and InteractAlternate action events from PlayerInputActions
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteraceAlternate.performed += InteractAlternate_performed;
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
