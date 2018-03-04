using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item {

    public override bool Use()
    {
        base.Use();
        return true;
    }
}
