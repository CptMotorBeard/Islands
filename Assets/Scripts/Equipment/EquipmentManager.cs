using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("More than one instance of Equipment Manager");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Equipment[] currentEquipment;
    Inventory inventory;

    void Start()
    {
        currentEquipment = new Equipment[System.Enum.GetNames(typeof(EquipmentType)).Length];
        inventory = Inventory.instance;
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentType;

        Equipment oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem, 1);
        }

        currentEquipment[slotIndex] = newItem;

        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem, oldItem);
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem, 1);

            currentEquipment[slotIndex] = null;
            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, oldItem);
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }
}
