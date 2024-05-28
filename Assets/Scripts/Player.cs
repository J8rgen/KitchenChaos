using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    [SerializeField] private float moveSpeed = 7f; // [SerializeField] makes it editable in the editor under player in inspect
    [SerializeField] private GameInput gameInput;

    private bool isWalking;

    private void Update(){

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
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                // Can move only on the X
                moveDir = moveDirX;
            }
            else {
                // Cannot move only on the X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized; //without normalized goes slower diagonal against 
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

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
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * roateSpeed); // with rotations use sleop, positions use lerp (takes a;b; t; choses a/b o inbweteen according to t value 0-1)

    }

    public bool IsWalking() {
        return isWalking;
    }

}
