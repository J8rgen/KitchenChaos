using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour{

    private const string IS_WALKING = "IsWalking";  // animation string

    [SerializeField] private Player player; // gameobject

    private Animator animator;
    private void Awake(){
        animator = GetComponent<Animator>();
    }
    
    
    private void Update(){
        animator.SetBool(IS_WALKING, player.IsWalking());
    }

}
