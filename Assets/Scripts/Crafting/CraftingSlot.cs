using UnityEngine.UI;
using UnityEngine;

public class CraftingSlot : MonoBehaviour
{
    [System.NonSerialized] public CraftingItem item;
    [System.NonSerialized] public bool selected = false;
    public CraftButton craft;

    public Sprite buttonSelect;
    public Sprite buttonRegular;

    // TODO: Make public colours for selected and not selected, or better yet make something that looks half decent

    public void Press()
    {
        selected = !selected;

        if (selected)
            CraftingManager.instance.Add(this);
        else
            CraftingManager.instance.Remove(this);

        if (selected)
            Select();
        else
            Clear();            

        craft.Clear();
    }

    public void Select()
    {
        this.gameObject.GetComponent<Image>().sprite = buttonSelect;
    }

    public void Clear()
    {
        this.gameObject.GetComponent<Image>().sprite = buttonRegular;
    }
}
