using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour {

    private bool isFirstUpdate = true; // A flag to check if it's the first update

    private void Update() {
        if (isFirstUpdate) {
            isFirstUpdate = false; // Set the flag to false after the first update

            Loader.LoaderCallback(); // Call the Loader callback to load the target scene

        }
    }

}
