using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject {

    public Item craftedItem;

    public CraftingItem[] recipe;

    void Start()
    {
        CraftingRecipeManager.instance.Add(this);
    }

    public bool Contains(CraftingItem item)
    {
        foreach (CraftingItem i in recipe)
        {
            if (i.item.id == item.item.id && item.quantity >= i.quantity)
                return true;
        }

        return false;
    }
}
