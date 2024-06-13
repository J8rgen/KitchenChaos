using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class ClearCounter : BaseCounter { // monobehavior base class, interface


    [SerializeField] private KitchenObjectSO kitchenObjectSO;


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
            if(player.HasKitchenObject()) {
                //player is carrying sth
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else {
                    //player is not carrying plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) { // check kitchen counter
                        //counter is holding a plate 
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject() .DestroySelf(); // destroy what the player is carrying
                        }
                    }
                }
            } 
            else {
                //player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


}
