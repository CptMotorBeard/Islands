using UnityEngine.UI;
using UnityEngine;

public class CraftingSlot : MonoBehaviour
{
    [System.NonSerialized] public CraftingItem item;
    [System.NonSerialized] public bool selected = false;
    public CraftButton craft;

    // TODO: Make public colours for selected and not selected, or better yet make something that looks half decent

    public void Press()
    {
        selected = !selected;

        if (selected)
            CraftingManager.instance.Add(this);
        else
            CraftingManager.instance.Remove(this);

        this.gameObject.GetComponent<Image>().color = selected ? Color.black : Color.white;
        craft.Clear();
    }
}
