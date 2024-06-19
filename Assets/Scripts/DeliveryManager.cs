using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeliveryManager : MonoBehaviour {


    public event EventHandler OnRecipeSpawned;   //update visual
    public event EventHandler OnRecipeCompleted; //update visual
    public event EventHandler OnRecipeSuccess;   //sound
    public event EventHandler OnRecipeFailed;    //sound

    public static DeliveryManager Instance { get; private set; } // used in soundManager


    [SerializeField] private RecipeListSO recipeListSO;


    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer; // current time value
    private float spawnRecipeTimerMax = 4f; //max time value
    private int waitingRecipeMax = 4; //maximum number of waiting recipes

    private void Awake() {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }



    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) { // if reaches 0
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count < waitingRecipeMax) {
                //spawn a new recipe:
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty); // update visual
            }

        }
    }


    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        // Loop through all waiting recipes
        for (int i=0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // Has the same number of ingredients
                bool plateContentsMatcheRecipe = true; // initialize

                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    //Cycling through all ingredients in the recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        //Cycling through all ingredients on the plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            //ingredient matches
                            ingredientFound=true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        // This recipe ingredient was not found on the Plate
                        plateContentsMatcheRecipe = false;
                    }
                }

                if (plateContentsMatcheRecipe) {
                    // Player delivered the correct recipe

                    // Remove the matched recipe from the waiting list
                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty); // visual
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);   // sound
                    return;
                }

            }

        }
        // No matches found!
        //Player did not deliver a correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty); //sound
    }


    // Method to get the list of waiting recipes
    public List<RecipeSO> GetWaintingRecipeSOList() {
        return waitingRecipeSOList;
    }




}
