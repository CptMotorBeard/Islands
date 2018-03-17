using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour {

    #region Singleton
    public static CraftingManager instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("An instance of Crafting Manager already exists");
            return;
        }

        instance = this;
    }
    #endregion

    public List<CraftingItem> recipe = new List<CraftingItem>();
    
    public void Add(CraftingItem item)
    {
        recipe.Add(item);
        UpdateList();
    }

    public void Remove(CraftingItem item)
    {
        recipe.Remove(item);
        UpdateList();
    }

    void UpdateList()
    {

    }
}
