using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeliveryManager : MonoBehaviour {

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    private void Awake() {
        waitingRecipeSOList = new List<RecipeSO>();
    }



    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count < waitingRecipeMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);


            }

        }
    }


    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for (int i=0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // Has the same number of ingredients
                bool plateContentsMatcheRecipe = true;

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
                    Debug.Log(("Player delivered the correct recipe"));
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }

            }

        }
        // No matches found!
        //Player did not deliver a correct recipe
        Debug.Log("Player did not deliver a correct recipe");
    }


}
