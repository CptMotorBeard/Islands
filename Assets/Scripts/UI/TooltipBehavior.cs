using UnityEngine;
using TMPro;

public class TooltipBehavior : MonoBehaviour
{

    #region Singleton
    public static TooltipBehavior instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Another instance of tooltip behavior already exists");
            return;
        }

        instance = this;
    }
    #endregion

    public GameObject tooltip;
    TextMeshProUGUI tooltipText;

    void Start()
    {
        tooltipText = tooltip.GetComponent<TextMeshProUGUI>();
        tooltipText.text = "";
        tooltip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tooltip.activeSelf)
            tooltip.transform.position = Input.mousePosition + new Vector3(7, 10, 0);
    }

    public void SetTooltip(string message)
    {
        tooltipText.text = message;
        tooltip.SetActive(true);
    }

    public void ClearTooltip()
    {
        tooltipText.text = "";
        tooltip.SetActive(false);
    }
}
