using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour {


    // you can see all of them on soundManager
    private void Awake() {
        // Clear all listeners on...
        CuttingCounter.ResetStaticData(); 
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();

    }


}
