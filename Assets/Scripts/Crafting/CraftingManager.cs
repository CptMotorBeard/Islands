using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour {

    #region Singleton
    public static CraftingManager instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("An instance of Crafting Manager already exists");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnRecipeUpdate();
    public OnRecipeUpdate onRecipeUpdateCallback;

    List<CraftingSlot> slots = new List<CraftingSlot>();
    List<CraftingItem> recipe = new List<CraftingItem>();
    public List<CraftingRecipe> recipes = new List<CraftingRecipe>();

    public void Add(CraftingSlot craftingItem)
    {
        slots.Add(craftingItem);
        recipe.Add(craftingItem.item);
        UpdateList();
    }

    public void Remove(CraftingSlot craftingItem)
    {
        slots.Remove(craftingItem);
        recipe.Remove(craftingItem.item);
        UpdateList();
    }

    void UpdateList()
    {
        recipes = CraftingRecipeManager.instance.RecipeExists(recipe);

        if (onRecipeUpdateCallback != null)
            onRecipeUpdateCallback.Invoke();
    }

    public void UpdateCrafting()
    {
        recipe.Clear();

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item.quantity > 0)
            {
                recipe.Add(slots[i].item);
            }
            else
            {
                slots.RemoveAt(i);
            }
        }

        UpdateList();
    }
}
