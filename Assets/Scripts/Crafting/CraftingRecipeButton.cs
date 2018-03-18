using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeButton : MonoBehaviour {
    public CraftingRecipe recipe;

    public void Craft()
    {
        // Remove items from inventory
        Inventory.instance.Add(recipe.craftedItem, 1);
    }
}
