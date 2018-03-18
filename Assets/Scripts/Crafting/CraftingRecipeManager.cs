using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeManager : MonoBehaviour {

    #region Singleton
    public static CraftingRecipeManager instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("More than one instance of Crafting Recipe Manager");
            return;
        }

        instance = this;
    }
    #endregion

    public List<CraftingRecipe> recipes = new List<CraftingRecipe>();

    public void Add(CraftingRecipe recipe)
    {
        recipes.Add(recipe);
    }

    public List<CraftingRecipe> RecipeExists(List<CraftingItem> inRecipe)
    {
        List<CraftingRecipe>  outRecipe = new List<CraftingRecipe>();
        foreach (CraftingRecipe r in recipes)
        {
            bool validRecipe = (r.recipe.Length == inRecipe.Count);
            int i = r.recipe.Length;           
            while (validRecipe && i > 0)
            {
                validRecipe = Contains(inRecipe, r.recipe[--i]);                                
            }

            if (validRecipe)
            {
                outRecipe.Add(r);
            }
        }

        return outRecipe;
    }

    bool Contains(List<CraftingItem> recipe, CraftingItem item)
    {
        foreach (CraftingItem c in recipe)
        {           
            if (item.item.id == c.item.id && c.quantity >= item.quantity)
                return true;
        }
        return false;
    }
}
