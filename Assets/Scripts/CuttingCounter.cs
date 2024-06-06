using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            //There is no KitchenObject here
            if (player.HasKitchenObject()) {
                //Player is carrying sth
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else {
                //Player has nothing
            }

        }
        else {
            //there is a KitchenObject here.
            if (player.HasKitchenObject()) {
                //player is carrying sth
            }
            else {
                //player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


    public override void InteractAlternate(Player player) {
        if( HasKitchenObject()) {
            // If there is a KitchenObject
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKictchenObject(cutKitchenObjectSO, this);
        }
    }
}
