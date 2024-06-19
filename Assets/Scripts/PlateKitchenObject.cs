using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    // Event to update images on plate when an ingredient is added
    public event EventHandler <OnIngredientAddedEventArgs> OnIngredientAdded; // updates the visual


    // EventArgs class to hold kitchen object data
    public class OnIngredientAddedEventArgs : EventArgs { // updates the visual
        public KitchenObjectSO kitchenObjectSO;
    
    }

    // List of valid kitchen objects that can be added to the plate
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;


    // List of kitchen objects currently on the plate
    private List<KitchenObjectSO> kitchenObjectSOList;


    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO> ();
    }


    // Method to try to add an ingredient to the plate
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) { 
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO)) {
            // not a valid ingredient
            return false;
        }

        if( kitchenObjectSOList.Contains(kitchenObjectSO) ) {
            // already has this type
            return false;

        }
        else {
            // Add the ingredient to the plate
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
                kitchenObjectSO = kitchenObjectSO // initialize its fields ( this is passed to OnIngredientAddedinvoked)
            }) ;

            return true; // was successfully added
        }
    }


    //List of kitchen objects currently on plate
    public List<KitchenObjectSO> GetKitchenObjectSOList() {
        return kitchenObjectSOList;
    }

}
