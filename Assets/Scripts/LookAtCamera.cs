using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour{

    private enum Mode {
        LookAt, // Object will look at the camera
        LookAtInverted, // Object will look away from the camera (inverted)
        CameraForward, // align forward direction with the camera's forward direction
        CameraForwardInverted, //align forward direction opposite to the camera's forward direction
    }

    [SerializeField] private Mode mode; // chose a mode

    private void LateUpdate() {
        switch (mode) {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
        
    }
}
