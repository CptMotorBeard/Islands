using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentType equipmentType;

    // Equipment stats
    public int warmthMod;
    public int damageMod;
    public int armorMod;

    public override bool Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        return true;
    }

}

public enum EquipmentType { RING, HELMET, NECKLACE, GLOVE, CHEST, WEAPON, OFFHAND, BOOT }