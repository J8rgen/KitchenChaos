using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {


    public static event EventHandler OnAnyObjectPlacedHere;

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;


    public virtual void Interact(Player player) { //for every function that we want the child classes to implement it  
        Debug.LogError("Basecounter.Interact();"); // should never trigger (we override this)
    }

    
    public virtual void InteractAlternate(Player player) { //for every function that we want the child classes to implement it  
        Debug.Log("Basecounter.InteracAlternatet();"); // should never trigger (we override this)
    }


    public Transform GetKitchenObjectFollowTransform() { // return position where kitchen objects will be visually placed
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject; // place a kitchen object on the counter

        if (kitchenObject != null) { // if an object is placed
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() { // retrieve KitchenObject that is currently placed
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null; //  checks if the kitchenObject field is not null
    }
}
