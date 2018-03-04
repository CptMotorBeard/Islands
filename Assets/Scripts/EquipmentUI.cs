using UnityEngine;
using TMPro;

public class EquipmentUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject equipmentUI;
    public TextMeshProUGUI tooltip;

    EquipmentManager equipmentManager;
    EquipmentSlot[] slots;

    // Use this for initialization
    void Start()
    {
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentChanged += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Equipment"))
        {
            equipmentUI.SetActive(!equipmentUI.activeSelf);
            tooltip.text = "";
        }
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
