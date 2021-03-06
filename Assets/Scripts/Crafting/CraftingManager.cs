﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{

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
    public Dictionary<CraftingRecipe, int> recipes = new Dictionary<CraftingRecipe, int>();

    public void Add(CraftingSlot craftingItem)
    {
        slots.Add(craftingItem);
        // Because of how the UI works, crafting slots can be added or removed without changing the recipe.
        // We don't want to add an item to the crafting recipe again if it already exists.
        if (inRecipe(craftingItem.item.item.id))
            return;
        recipe.Add(craftingItem.item);
        UpdateList();
    }

    public void Remove(CraftingSlot craftingItem)
    {
        slots.Remove(craftingItem);
        UpdateCrafting();
    }

    public void Clear()
    {
        slots.Clear();
    }

    public bool inRecipe(int id)
    {
        foreach (CraftingItem c in recipe)
        {
            if (c.item.id == id)
                return true;
        }
        return false;
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

        foreach (CraftingSlot c in slots)
        {
            if (!inRecipe(c.item.item.id))
                recipe.Add(c.item);
        }

        UpdateList();
    }
}
