using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false); // iconTemplate set to off
    }




    public void SetRecipeSO(RecipeSO recipeSO) {
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer) {
            // Remove existing icons from the iconcontainer, exept icontemplate (which is off)
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        // Instantiate new icons for each kitchen object in the recipe
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList) {
            // Instantiate a new icon using the iconTemplate as the blueprint
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true); // becasue template is off
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
            

    }

}
