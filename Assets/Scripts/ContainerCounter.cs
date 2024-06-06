using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter {

    public event EventHandler OnplayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            //player is not carrying anything

            KitchenObject.SpawnKictchenObject(kitchenObjectSO, player);

            OnplayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }


}
