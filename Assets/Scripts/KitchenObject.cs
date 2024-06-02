using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO; 
    }


    public void SetClearCounter(ClearCounter clearCounter) { // when we set clear counter to a different one    
        if (this.clearCounter != null) {
            this.clearCounter.ClearKitchenObject();
        }

        this.clearCounter = clearCounter; //CAREFUL HERE WITH OLD AND NEW CLEAR COUNTER

        if (clearCounter.HasKitchenObject()) {
            Debug.LogError("Counter already has a KitchenObject");
        }
        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform(); // will teleport the items after setting different one
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter() {
        return clearCounter;
    }
}
