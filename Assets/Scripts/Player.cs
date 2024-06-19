using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent{


    public static Player Instance { get; private set; }


    // Event triggered when player picks something
    public event EventHandler OnPickedSomething;  // sound
    // Event triggered when selected counter changes
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged; //generics

    //arguments for OnSelectedCounterChanged event
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;                // Movement speed of the player
    [SerializeField] private GameInput gameInput;                 // Reference to the GameInput scriptableObject
    [SerializeField] private LayerMask countersLayerMask;         // Layer mask to interact with counters
    [SerializeField] private Transform kitchenObjectHoldPoint;    // Point from which kitchen objects are held


    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;


    private void Awake() {
        // Ensure there is only one instance of Player
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }


    private void Start() {
        // Subscribe to input events
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if(selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void Update(){
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions() {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized(); // normalized same speed in diagonal
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) { // if direction isnt 0 will contain last movement direction
            lastInteractDir = moveDir; // even if we stop moving we will still use last movement direction for interact
        }

        float interactDistance = 2;
        //if we hit something: // can also use raycastall if need an array of objects behind eachother
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) { //origin, direction, raycast hit, distance  (RC go to definition)

            //if interact hits ClearCounter
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) { 
                //clearCounter.Interact();
                if (baseCounter != selectedCounter) { // if not already
                    selectedCounter = baseCounter;

                    SetSelectedCounter(baseCounter);
                }
            }
            else {
                SetSelectedCounter(null);
            }

        }
        else { // if nothing infront of player
            SetSelectedCounter(null);
        }
    }


    private void HandleMovement() {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //for collision check
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        //bool canMove = !Physics.Raycast(transform.position, moveDir, playerRadius); //from where, direction, distance // true if hit something, false if nothing
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        //.CapsuleCast()   bottom of captule, top of captule, radius of player


        //diagonal movement against objects
        if (!canMove) {
            // Cannot move towards moveDir

            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized; //without normalized goes slower diagonal against 
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                // Can move only on the X
                moveDir = moveDirX;
            }
            else {
                // Cannot move only on the X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized; //without normalized goes slower diagonal against 
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    // Can move only on the z
                    moveDir = moveDirZ;
                }
                else {
                    // Cannot move in any direction
                }
            }
        }


        if (canMove) { //collision check, needs box collider on objects
            transform.position += moveDir * moveDistance; //seconds elapsed since last frame (so it isnt tied to frames)
        }

        isWalking = moveDir != Vector3.zero; //if direction is not 0 0 0


        //transform.eulerAngles 1-360
        //transform.LookAt  look at a target/ enemy
        //transform.up transform.right
        //transform.forward - you can read and write this (get direction or set direction)
        float roateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * roateSpeed); // with rotations use slerp, positions use lerp (takes a;b; t; choses a/b o inbweteen according to t value 0-1)

    }


    // Set the selected kitchen counter
    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;


        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter //hover over to see which is which
        });
    }


    // Get the transform for kitchen object follow point
    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    // Set the current kitchen object held by player
    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) { // play sound
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }


    // Get the current kitchen object held by player
    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    // Clear the current kitchen object held by player
    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    // Check if player currently has a kitchen object
    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
