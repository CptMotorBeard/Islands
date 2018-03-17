using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "Debug Item Name";
    public Sprite icon = null;
    public int maxQuantity = 999;

    public int id;
    public bool craftingItem;


    public virtual bool Use()
    {
        MessageManagement.instance.SetMessage(name + " was used");
        Debug.Log(name + " was used");
        return false;
    }
}
