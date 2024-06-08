using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter {

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalized;

    }

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecepeSO[] cuttingRecepeSOArray;

    private int cuttingProgress;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            //There is no KitchenObject here
            if (player.HasKitchenObject()) {
                //Player is carrying sth
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // Playter carrying something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecepeSO cuttingRecepeSO = GetCuttingRecepeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecepeSO.cuttingProgressMax
                    });
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
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecepeSO cuttingRecepeSO = GetCuttingRecepeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress / cuttingRecepeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecepeSO.cuttingProgressMax) {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKictchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecepeSO cuttingRecepeSO = GetCuttingRecepeSOWithInput(inputKitchenObjectSO);
        return cuttingRecepeSO != null; 
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecepeSO cuttingRecepeSO = GetCuttingRecepeSOWithInput(inputKitchenObjectSO);
        if(cuttingRecepeSO != null) {
            return cuttingRecepeSO.output;
        } 
        else { 
            return null; 
        }
    
    }

    private CuttingRecepeSO GetCuttingRecepeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecepeSO cuttingRecepeSO in cuttingRecepeSOArray) {
            if (cuttingRecepeSO.input == inputKitchenObjectSO) {
                return cuttingRecepeSO;
            }
        }
        return null;
    }
}

