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
        transform.position += moveDir * moveSpeed * Time.deltaTime; //seconds elapsed since last frame (so it isnt tied to frames)

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
