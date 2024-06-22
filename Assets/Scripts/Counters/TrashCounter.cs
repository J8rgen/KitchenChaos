using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter {

    public static event EventHandler OnAnyObjectTrashed; //sound

    new public static void ResetStaticData() { // new so we dont get a warning
        OnAnyObjectTrashed = null;
    }


    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }

    }
}
