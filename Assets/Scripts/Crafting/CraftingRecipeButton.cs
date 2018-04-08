using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingRecipeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CraftingRecipe recipe;
    public CraftButton craft;
    public int maxQuantity;

    public void Craft()
    {
        craft.SetButton(this);
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
