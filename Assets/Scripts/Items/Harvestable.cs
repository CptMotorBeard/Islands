using UnityEngine;

public class Harvestable : Interactable {
    public float respawnTime;

    public ToolType toolType;
    public int requiredGrade;

    bool active = true;
    float currentWait;
    bool toolRequired;

    public Item item;
    public int maxQuantity;
    int quantity;


    Vector2 originalPosition;
    private int shakeCount = 0;

    void Start()
    {
        originalPosition = transform.position;
        quantity = maxQuantity;
        toolRequired = toolType != ToolType.NONE;
    }

    void Update()
    {
        BaseUpdate();
    }
    
    public override void BaseUpdate()
    {

        // TODO: Maybe make all interactables respawn
        if (!active)
        {
            currentWait += Time.deltaTime;
            if (currentWait >= respawnTime)
            {
                active = true;
                quantity = maxQuantity;
                ToggleComponents();
            }
        }
        else
        {
            base.BaseUpdate();

            if (shakeCount > 0)
            {
                Vector2 newPosition = Random.insideUnitCircle / 20;
                transform.position = new Vector2(transform.position.x + newPosition.x, transform.position.y + newPosition.y);
                shakeCount--;

                if (shakeCount <= 0)
                {
                    transform.position = originalPosition;
                }
            }
        }
    }

    public override void Interact()
    {
        InventoryItem selectedItem = ToolbarManager.instance.GetSelectedItem();
        Tool selectedTool = null;

        if (selectedItem != null && selectedItem.item.GetType() == typeof(Tool))
            selectedTool = (Tool)selectedItem.item;

        if (toolType == ToolType.NONE || (selectedTool != null && (selectedTool.toolType == toolType && selectedTool.grade >= requiredGrade)))
        {
            toolRequired = toolType != ToolType.NONE;
            base.Interact();

            Harvest();
        }
        else
        {
            MessageManagement.instance.SetErrorMessage("You don't have the right tool for the job");
            toolRequired = false;
        }
    }

    public void Harvest()
    {
        if (Inventory.instance.Add(item, 1))
        {
            quantity--;
            shakeCount = 6;

            if (quantity <= 0)
            {                
                active = false;
                currentWait = 0f;
                ToggleComponents();
            }
        }            
    }

    public void ToggleComponents()
    {
        gameObject.GetComponent<Renderer>().enabled = active;
        Collider2D[] colliders = gameObject.GetComponents<Collider2D>();
        
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = active;
        }

        shakeCount = 0;
        PathfindingGrid.instance.UpdateGrid(this.transform.position, 5);
    }

    public override bool ToolRequired()
    {
        return toolRequired;
    }

    public override bool isActive()
    {
        return active;
    }
}
