using UnityEngine;
using TMPro;

public class GameplayMenuUI : MonoBehaviour {

    public GameObject gameplayMenu;
    public GameObject inventoryMenu;
    public GameObject equipmentMenu;
    public GameObject craftingMenu;
    public TextMeshProUGUI menuText;

    Vector3 defaultPosition;

    void Start()
    {
        defaultPosition = gameplayMenu.transform.position;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (!gameplayMenu.activeSelf || inventoryMenu.activeSelf)
                ToggleMenu();
            SetActiveMenu("Inventory");            
        }

        if (Input.GetButtonDown("Equipment"))
        {
            if (!gameplayMenu.activeSelf || equipmentMenu.activeSelf)
                ToggleMenu();
            SetActiveMenu("Equipment");            
        }

        if (Input.GetButtonDown("Crafting"))
        {
            if (!gameplayMenu.activeSelf || craftingMenu.activeSelf)
                ToggleMenu();
            SetActiveMenu("Crafting");
        }

        if (Input.GetButtonDown("ResetMenu"))
        {
            gameplayMenu.transform.position = defaultPosition;
        }
    }

    public void ToggleMenu()
    {
        TooltipBehavior.instance.ClearTooltip();
        InventoryManager.instance.ReturnItem();
        gameplayMenu.SetActive(!gameplayMenu.activeSelf);
    }

    public void SetActiveMenu(string menuName)
    {
        menuText.text = menuName;

        switch (menuName)
        {
            case "Inventory":
                inventoryMenu.SetActive(true);
                equipmentMenu.SetActive(false);
                craftingMenu.SetActive(false);
                break;
            case "Equipment":
                inventoryMenu.SetActive(false);
                equipmentMenu.SetActive(true);
                craftingMenu.SetActive(false);
                break;
            case "Crafting":
                inventoryMenu.SetActive(false);
                equipmentMenu.SetActive(false);
                craftingMenu.SetActive(true);
                break;
            default:
                Debug.LogWarning("Invalid menu opening attempt");
                return;
        }
    }
}
