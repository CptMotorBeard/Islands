using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftButton : MonoBehaviour
{

    CraftingRecipe recipe;
    Button craftButton;
    int max = 1;
    int quantity = 1;

    public TextMeshProUGUI qtext;

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
            craftButton.gameObject.GetComponent<Image>().color = Color.white;
            if (craftButton == button.gameObject.GetComponent<Button>())
            {
                craftButton = null;
                return;
            }
        }
        recipe = button.recipe;
        max = button.maxQuantity;
        craftButton = button.gameObject.GetComponent<Button>();
        craftButton.gameObject.GetComponent<Image>().color = Color.black;
    }

    public void Clear()
    {
        recipe = null;
        if (craftButton)
            craftButton.gameObject.GetComponent<Image>().color = Color.white;
        craftButton = null;
    }
}
