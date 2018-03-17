
public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        Pickup();
    }

    void Pickup()
    {
        if (Inventory.instance.Add(item, 1))
            Destroy(gameObject);
    }
}
