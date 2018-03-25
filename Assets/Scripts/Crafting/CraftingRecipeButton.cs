using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingRecipeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CraftingRecipe recipe;

    public void Craft()
    {
        // As of right now, even though the items are removed from the inventory,
        // You cannot craft with a full inventory.
        // I may change this so:

        // TODO: Remove items from inventory before trying to craft
        if (Inventory.instance.Add(recipe.craftedItem, recipe.quantity))
        {
            foreach (CraftingItem c in recipe.recipe)
            {
                Inventory.instance.RemoveItem(c.item, c.quantity);
            }

            CraftingManager.instance.UpdateCrafting();
            TooltipBehavior.instance.ClearTooltip();
        }        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        string tooltip = "";
        foreach (CraftingItem c in recipe.recipe)
        {
            tooltip += c.item.name + " :" + c.quantity;
            tooltip += ", ";
        }
        TooltipBehavior.instance.SetTooltip(tooltip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipBehavior.instance.ClearTooltip();
    }
}
