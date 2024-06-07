using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private CuttingRecepeSO[] cuttingRecepeSOArray;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            //There is no KitchenObject here
            if (player.HasKitchenObject()) {
                //Player is carrying sth
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // Playter carrying something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                }
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
        if( HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            // If there is a KitchenObject AND it can be cut
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKictchenObject(outputKitchenObjectSO, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecepeSO cuttingRecepeSO in cuttingRecepeSOArray) {
            if (cuttingRecepeSO.input == inputKitchenObjectSO) {
                return true;
            }
        }
        return false;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecepeSO cuttingRecepeSO in cuttingRecepeSOArray) {
            if (cuttingRecepeSO.input == inputKitchenObjectSO) {
                return cuttingRecepeSO.output;
            }
        }
        return null;
    }
}
