using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "Debug Item Name";
    public Sprite icon = null;
    public int maxQuantity = 999;

    public int id;

    public virtual bool Use()
    {
        Debug.Log(name + " was used");
        return false;
    }
}
