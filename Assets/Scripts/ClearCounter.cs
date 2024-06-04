using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter { // monobehavior base class, interface


    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    //[SerializeField] private ClearCounter secondClearCounter;
    //[SerializeField] private bool testing; testing


    //private void Update() { OLD TESTING CODE
    //    if (testing && Input.GetKeyDown(KeyCode.T)) {
    //        if (kitchenObject != null) {
    //            kitchenObject.SetKitchenObjectParent(secondClearCounter); // set new parent for object
    //        }
    //    }
    //}

    public override void Interact(Player player) {

    }
}
