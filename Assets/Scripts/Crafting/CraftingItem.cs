using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CraftingItem
{
    [SerializeField] public Item item;
    [SerializeField] public int quantity;

    public CraftingItem(Item i, int q)
    {
        this.item = i;
        this.quantity = q;
    }
}
