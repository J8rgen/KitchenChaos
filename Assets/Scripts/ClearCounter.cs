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
            } 
            else {
                //player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


    public override void InteractAlternate(Player player) { // I ADDED THIS 
        //NOTHING 
    }


}
