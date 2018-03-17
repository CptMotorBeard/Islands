using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class Food : Consumable {

    public float healthRestored;
    public float hungerRestored;

    public override bool Use()
    {
        bool consumed = base.Use();

        PlayerStatus.instance.LoseEnergy(-hungerRestored);
        PlayerStatus.instance.LoseHealth(-healthRestored);
        return consumed;
    }
}
