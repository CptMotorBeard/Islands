using UnityEngine.UI;
using UnityEngine;

public class CraftingSlot : MonoBehaviour {

    public CraftingItem item;
    bool selected = false;

    public void Press()
    {
        selected = !selected;

        if (selected)
            CraftingManager.instance.Add(item);
        else
            CraftingManager.instance.Remove(item);
        
        this.gameObject.GetComponent<Image>().color = selected ? Color.black : Color.white;        
    }
}
