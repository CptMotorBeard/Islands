using UnityEngine;
using System.Collections.Generic;

public class DebugItems : MonoBehaviour {

    public Dictionary<int, Item> m_ItemList = new Dictionary<int, Item>();

    #region Singleton
    public static DebugItems instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Another instance of debug items exists");
            return;
        }
        instance = this;

        Item[] items = Resources.FindObjectsOfTypeAll<Item>();

        foreach (Item i in items)
        {
            m_ItemList.Add(i.id, i);
        }
    }
    #endregion
    
}