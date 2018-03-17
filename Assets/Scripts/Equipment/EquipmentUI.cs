using UnityEngine;
using TMPro;

public class EquipmentUI : MonoBehaviour {

    public Transform itemsParent;

    EquipmentManager equipmentManager;
    EquipmentSlot[] slots;

    void Start()
    {
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentChanged += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    void UpdateUI(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            slots[(int)newItem.equipmentType].EquipItem(newItem);
        }
        else
        {
            slots[(int)oldItem.equipmentType].RemoveItem();
        }        
    }
}
