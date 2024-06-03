using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent { // monobehavior base class, interface


    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    //[SerializeField] private ClearCounter secondClearCounter;
    //[SerializeField] private bool testing; testing

    private KitchenObject kitchenObject;

    //private void Update() { OLD TESTING CODE
    //    if (testing && Input.GetKeyDown(KeyCode.T)) {
    //        if (kitchenObject != null) {
    //            kitchenObject.SetKitchenObjectParent(secondClearCounter); // set new parent for object
    //        }
    //    }
    //}

    public void Interact(Player player) {
        if (kitchenObject == null) { // so we dont spawn infinite objects
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        } else {
            //give the object to the player
            kitchenObject.SetKitchenObjectParent(player);
        }
    }


    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

}
