using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // Interface for kitchen object parent
    private IKitchenObjectParent kitchenObjectParent;


    // Method to get the KitchenObjectSO
    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO; 
    }


    // Method to set the kitchen object parent
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {   // always check if free

        // Check if the new parent already has a kitchen object (it should never have, always check)
        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("IKitchenObjectParent already has a KitchenObject");
        }

        if (this.kitchenObjectParent != null) { // Clear the kitchen object from the current parent, if it exists (e.g. player)
            this.kitchenObjectParent.ClearKitchenObject();
        }

        // Set the new parent (player <-> counter)
        this.kitchenObjectParent = kitchenObjectParent;

        // Assign this kitchen object to the new parent
        kitchenObjectParent.SetKitchenObject(this);

        // Set the kitchen object's transform to follow the new parent's transform
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform(); 
        transform.localPosition = Vector3.zero; // Reset position to local zero
    }


    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }

    public void DestroySelf() {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if ( this is PlateKitchenObject ) {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else { 
            plateKitchenObject = null;
            return false; 
        }
    }



    public static KitchenObject SpawnKictchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent KitchenObjectParent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab); //Instantiate the Prefab
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();  //Get the KitchenObject Component
        //necessary to access and manipulate the component's properties and methods

        kitchenObject.SetKitchenObjectParent(KitchenObjectParent);
        return kitchenObject;
    }
}
