using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    #region Singleton
    public static InventoryManager instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Another instance of inventory manager exists");
            return;
        }
        instance = this;
    }
    #endregion

    public InventoryItem currentItem;
    public GameObject imageTransform;
    public Image sprite;
    TextMeshProUGUI heldQuantity;

    void Start()
    {
        heldQuantity = imageTransform.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (imageTransform.activeSelf)
        {
             imageTransform.transform.position = Input.mousePosition + new Vector3(2, 2, 0);
        }
    }

    public void PickupItem(InventoryItem newItem)
    {
        imageTransform.SetActive(true);
        currentItem = newItem;
        sprite.sprite = newItem.item.icon;
        heldQuantity.text = currentItem.quantity.ToString();
    }

    public void PickupItem (int quantity)
    {
        imageTransform.SetActive(true);
        currentItem.quantity += quantity;
        heldQuantity.text = currentItem.quantity.ToString();
    }

    public void DropItem (int quantity)
    {      
        if (quantity >= currentItem.quantity)
            ClearItem();
        else
        {
            currentItem.quantity -= quantity;
            heldQuantity.text = currentItem.quantity.ToString();
        }
    }

    public void ClearItem()
    {
        imageTransform.SetActive(false);
        currentItem = null;
        sprite.sprite = null;
    }
}
