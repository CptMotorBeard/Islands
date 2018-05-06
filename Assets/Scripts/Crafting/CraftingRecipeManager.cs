using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeManager : MonoBehaviour
{

    #region Singleton
    public static CraftingRecipeManager instance;
    List<CraftingRecipe> recipes = new List<CraftingRecipe>();

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("More than one instance of Crafting Recipe Manager");
            return;
        }

        CraftingRecipe[] allRecipes  = Resources.LoadAll<CraftingRecipe>("Crafting");
        
        foreach (CraftingRecipe c in allRecipes)
        {
            recipes.Add(c);
        }

        instance = this;
    }
    #endregion

    public void Add(CraftingRecipe recipe)
    {
        recipes.Add(recipe);
    }

    // Returns a list of valid recipes, given the quantities and items provided by inRecipe
    public Dictionary<CraftingRecipe, int> RecipeExists(List<CraftingItem> inRecipe)
    {
        Dictionary<CraftingRecipe, int> outRecipe = new Dictionary<CraftingRecipe, int>();
        foreach (CraftingRecipe r in recipes)
        {
            
            bool validRecipe = (r.recipe.Length == inRecipe.Count);
            int i = r.recipe.Length;
            int maxQuantity = 999;
            while (validRecipe && i > 0)
            {
                int q = 999;
                validRecipe = Contains(inRecipe, r.recipe[--i], out q);
                maxQuantity = Mathf.Min(q, maxQuantity);
            }

            if (validRecipe)
            {
                outRecipe.Add(r, maxQuantity);
            }
        }

        return outRecipe;
    }

    bool Contains(List<CraftingItem> recipe, CraftingItem item, out int maxQuantity)
    {
        maxQuantity = 0;
        foreach (CraftingItem c in recipe)
        {
            if (item.item.id == c.item.id && c.quantity >= item.quantity)
            {
                maxQuantity = c.quantity / item.quantity;
                return true;
            }                
        }
        return false;
    }
}
