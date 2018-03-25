using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{

    public Item craftedItem;
    public int quantity = 1;

    public CraftingItem[] recipe;
}
