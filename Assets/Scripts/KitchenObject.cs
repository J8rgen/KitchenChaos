using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO; 
    }


    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {   

        if (this.kitchenObjectParent != null) { // Clear the kitchen object from the current parent, if it exists (like player for example)
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent; 

        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("IKitchenObjectParent already has a KitchenObject");
        }
        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform(); // will teleport the items after setting different one
        transform.localPosition = Vector3.zero;
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
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            
        kitchenObject.SetKitchenObjectParent(KitchenObjectParent);
        return kitchenObject;
    }
}
