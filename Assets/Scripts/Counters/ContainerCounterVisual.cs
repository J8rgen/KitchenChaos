using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour {

    private const string OPEN_CLOSE = "OpenClose"; // animator animation string 

    [SerializeField] private ContainerCounter containerCounter;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }


    private void Start() {
        containerCounter.OnplayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject; // subscription call
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
