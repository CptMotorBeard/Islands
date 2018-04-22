﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftButton : MonoBehaviour
{

    CraftingRecipe recipe;
    Button craftButton;
    int max = 1;
    int quantity = 1;

    public TextMeshProUGUI qtext;
    public Sprite buttonSelect;
    public Sprite buttonNormal;

    void Start()
    {
        qtext.text = quantity.ToString();
        recipe = null;
        craftButton = null;
    }

    public void Craft()
    {
        // As of right now, even though the items are removed from the inventory,
        // You cannot craft with a full inventory.
        // I may change this so:
        // TODO: Remove items from inventory before trying to craft
        if (Inventory.instance.Add(recipe.craftedItem, quantity * recipe.quantity))
        {
            foreach (CraftingItem c in recipe.recipe)
            {
                Inventory.instance.RemoveItem(c.item, quantity * c.quantity);
            }

            CraftingManager.instance.UpdateCrafting();
            TooltipBehavior.instance.ClearTooltip();
        }
    }

    public void ChangeQuantity(int q)
    {
        quantity += q;
        quantity = Mathf.Clamp(quantity, 1, max);
        qtext.text = quantity.ToString();
    }

    public void SetButton(CraftingRecipeButton button)
    {
        if (recipe != null)
        {
            recipe = null;
            craftButton.gameObject.GetComponent<Image>().sprite = buttonNormal;
            if (craftButton == button.gameObject.GetComponent<Button>())
            {
                craftButton = null;
                return;
            }
        }
        recipe = button.recipe;
        max = Mathf.Min(button.maxQuantity, recipe.craftedItem.maxQuantity);
        if (quantity > max)
            quantity = max;
        qtext.text = quantity.ToString();
        craftButton = button.gameObject.GetComponent<Button>();
        craftButton.gameObject.GetComponent<Image>().sprite = buttonSelect;
    }

    public void Clear()
    {
        quantity = 1;
        qtext.text = quantity.ToString();
        recipe = null;
        if (craftButton)
            craftButton.gameObject.GetComponent<Image>().sprite = buttonNormal;
        craftButton = null;
    }
}