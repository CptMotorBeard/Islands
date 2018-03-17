using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : Interactable {

    public float respawnTime;
    bool active = true;
    float currentWait;

    public Item item;
    public int maxQuantity;
    int quantity;


    Vector2 originalPosition;
    private int shakeCount = 0;

    void Start()
    {
        originalPosition = transform.position;
        quantity = maxQuantity;
    }

    void Update()
    {
        BaseUpdate();
    }
    
    public override void BaseUpdate()
    {
        // May change this to all interactables (respawning)
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
        base.Interact();

        Harvest();
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
    }
}
