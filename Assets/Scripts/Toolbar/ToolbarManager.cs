using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarManager : MonoBehaviour
{

    #region Singleton
    public static ToolbarManager instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Another instance of toolbar already exists");
            return;
        }
        instance = this;
    }
    #endregion

    const int size = 10;
    int selected = 0;

    public Transform toolbarParent;
    public delegate void OnSelectionChange(int newIndex);
    public OnSelectionChange onSelectionChangeCallback;

    void Start()
    {
        UpdateSelected();
    }

    void Update()
    {
        if (DebugManager.instance.debug)
            return;

        // quick loop to check all num keys
        for (int i = 0; i < size; i++)
        {
            if (Input.GetKeyDown("" + i))
            {
                selected = i;
                if (--selected < 0)
                    selected = size-1;
                UpdateSelected();
                break;
            }
        }

        // scroll wheel to change slots + boundries
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScroll > 0)
        {
            if (++selected >= size)
                selected = 0;

            UpdateSelected();
        }
        else if (mouseScroll < 0)
        {
            if (--selected < 0)
                selected = size - 1;

            UpdateSelected();
        }
    }

    public void SelectSlot(int index)
    {
        selected = index;
        UpdateSelected();
    }

    void UpdateSelected()
    {
        if (onSelectionChangeCallback != null)
            onSelectionChangeCallback.Invoke(selected);
    }
}
