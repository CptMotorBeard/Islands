using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeButton : MonoBehaviour {
    public CraftingRecipe recipe;

    public void Craft()
    {
        foreach (CraftingItem c in recipe.recipe)
        {
            Inventory.instance.RemoveItem(c.item, c.quantity);
        }        
        Inventory.instance.Add(recipe.craftedItem, 1);
    }
}
